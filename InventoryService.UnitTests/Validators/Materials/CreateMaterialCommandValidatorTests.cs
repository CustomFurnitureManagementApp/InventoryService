using FluentValidation.TestHelper;
using InventoryService.Application.Features.Materials.Commands.CreateMaterial;
using InventoryService.Domain.Enums;

namespace InventoryService.UnitTests.Validators.Materials
{
    public class CreateMaterialCommandValidatorTests
    {
        private readonly CreateMaterialCommandValidator _validator;

        public CreateMaterialCommandValidatorTests()
        {
            _validator = new CreateMaterialCommandValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Name_Is_Empty()
        {
            var command = new CreateMaterialCommand { Name = "" };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.Name);
        }

        [Fact]
        public void Should_Have_Error_When_Code_Is_Too_Long()
        {
            var command = new CreateMaterialCommand { Code = new string('A', 51) };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.Code);
        }

        [Fact]
        public void Should_Not_Have_Error_For_Valid_Command()
        {
            var command = new CreateMaterialCommand
            {
                Name = "White MDF 18mm",
                Code = "MDF18W",
                MaterialType = MaterialType.MDF,
                UnitOfMeasure = UnitOfMeasure.Piece
            };

            var result = _validator.TestValidate(command);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
