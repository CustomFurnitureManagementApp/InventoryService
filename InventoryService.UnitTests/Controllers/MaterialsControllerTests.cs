using AutoFixture;
using AutoFixture.AutoMoq;
using InventoryService.Api.Controllers;
using InventoryService.Application.Common;
using InventoryService.Application.Features.Materials.Commands.CreateMaterial;
using InventoryService.Application.Features.Materials.Queries.GetMaterialById;
using InventoryService.Application.Features.Materials.Queries.GetMaterials;
using InventoryService.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace InventoryService.UnitTests.Controllers
{
    public class MaterialsControllerTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IMediator> _mediatorMock;
        private readonly MaterialsController _controller;

        public MaterialsControllerTests()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
            _mediatorMock = new Mock<IMediator>();
            _controller = new MaterialsController(_mediatorMock.Object);
        }

        [Fact]
        public async Task Get_ReturnsOk_WithMaterialsList()
        {
            // Arrange
            var materials = new List<GetMaterialsResponse>
            {
                new GetMaterialsResponse
                {
                    Id = 1,
                    Code = "MDF18W",
                    Name = "White MDF 18mm",
                    MaterialType = MaterialType.MDF,
                    UnitOfMeasure = UnitOfMeasure.Piece,
                    Specification = "Smooth"
                },
                new GetMaterialsResponse
                {
                    Id = 2,
                    Code = "PAL18OAK",
                    Name = "Oak Pal 18mm",
                    MaterialType = MaterialType.PAL,
                    UnitOfMeasure = UnitOfMeasure.Piece,
                    Specification = "Oak texture"
                }
            };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetMaterialsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result<IReadOnlyList<GetMaterialsResponse>>.Success(materials));

            // Act
            var result = await _controller.GetAllMaterials();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returned = Assert.IsType<List<GetMaterialsResponse>>(okResult.Value);
            Assert.Equal(2, returned.Count);

            // Verify first material properties
            var first = returned[0];
            Assert.Equal(1, first.Id);
            Assert.Equal("MDF18W", first.Code);
            Assert.Equal("White MDF 18mm", first.Name);
            Assert.Equal(MaterialType.MDF, first.MaterialType);
            Assert.Equal(UnitOfMeasure.Piece, first.UnitOfMeasure);
            Assert.Equal("Smooth", first.Specification);
        }

        [Fact]
        public async Task Get_ReturnsOk_WithEmptyList_WhenNoMaterials()
        {
            // Arrange
            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetMaterialsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result<IReadOnlyList<GetMaterialsResponse>>.Success(new List<GetMaterialsResponse>()));

            // Act
            var result = await _controller.GetAllMaterials();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returned = Assert.IsType<List<GetMaterialsResponse>>(okResult.Value);
            Assert.Empty(returned);
        }

        [Fact]
        public async Task CreateMaterial_ReturnsCreatedResult_WhenValidCommand()
        {
            // Arrange
            var command = new CreateMaterialCommand
            {
                Code = "MDF18W",
                Name = "White MDF 18mm",
                MaterialType = MaterialType.MDF,
                UnitOfMeasure = UnitOfMeasure.Piece,
                Specification = "Smooth",
                LengthMm = 2800,
                WidthMm = 2070
            };

            var response = new CreateMaterialResponse
            {
                Id = 1,
                Code = "MDF18W",
                Name = "White MDF 18mm",
                MaterialType = MaterialType.MDF,
                UnitOfMeasure = UnitOfMeasure.Piece,
                Specification = "Smooth",
                LengthMm = 2800,
                WidthMm = 2070
            };
            var mediatorResult = Result<CreateMaterialResponse>.Success(response);

            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateMaterialCommand>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(mediatorResult);

            // Act
            var result = await _controller.CreateMaterial(command);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var returned = Assert.IsType<CreateMaterialResponse>(createdResult.Value);

            // Check all properties
            Assert.Equal(1, returned.Id);
            Assert.Equal("MDF18W", returned.Code);
            Assert.Equal("White MDF 18mm", returned.Name);
            Assert.Equal(MaterialType.MDF, returned.MaterialType);
            Assert.Equal(UnitOfMeasure.Piece, returned.UnitOfMeasure);
            Assert.Equal("Smooth", returned.Specification);
            Assert.Equal(2800, returned.LengthMm);
            Assert.Equal(2070, returned.WidthMm);
        }

        [Fact]
        public async Task CreateMaterial_Throws_WhenMediatorThrowsException()
        {
            // Arrange
            var command = _fixture.Build<CreateMaterialCommand>().Create();

            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateMaterialCommand>(), It.IsAny<CancellationToken>()))
                         .ThrowsAsync(new InvalidOperationException("Mediator error"));

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _controller.CreateMaterial(command));
        }

        [Fact]
        public async Task CreateMaterial_ReturnsBadRequest_WhenCommandIsNull()
        {
            // Act
            var result = await _controller.CreateMaterial(null!);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task GetMaterialById_ReturnsOk_WhenMaterialExists()
        {
            // Arrange
            int materialId = 1;
            var material = new GetMaterialByIdResponse
            {
                Id = materialId,
                Code = "MDF18W",
                Name = "White MDF 18mm",
                MaterialType = MaterialType.MDF,
                UnitOfMeasure = UnitOfMeasure.Piece,
                Specification = "Smooth"
            };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetMaterialByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result<GetMaterialByIdResponse>.Success(material));

            // Act
            var result = await _controller.GetMaterialById(materialId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returned = Assert.IsType<GetMaterialByIdResponse>(okResult.Value);
            Assert.Equal(materialId, returned.Id);
            Assert.Equal("MDF18W", returned.Code);
        }

        [Fact]
        public async Task GetMaterialById_ReturnsNotFound_WhenMaterialDoesNotExist()
        {
            // Arrange
            int materialId = 99;

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetMaterialByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result<GetMaterialByIdResponse>.Failure("Material not found"));

            // Act
            var result = await _controller.GetMaterialById(materialId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            var returned = Assert.IsType<string>(notFoundResult.Value);
            Assert.Equal("Material not found", returned);
        }


    }
}
