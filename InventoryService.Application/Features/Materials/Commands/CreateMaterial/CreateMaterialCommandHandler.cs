using InventoryService.Domain.Interfaces;
using MediatR;

namespace InventoryService.Application.Features.Material.Commands.CreateMaterial
{
    public class CreateMaterialCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateMaterialCommand, CreateMaterialResponse>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<CreateMaterialResponse> Handle(CreateMaterialCommand request, CancellationToken cancellationToken)
        {
            var material = new Domain.Entities.Master.Material
            {
                Code = request.Code,
                Name = request.Name,
                MaterialType = request.MaterialType,
                Specification = request.Specification,
                LengthMm = request.LengthMm,
                WidthMm = request.WidthMm,
                UnitOfMeasure = request.UnitOfMeasure
            };

            await _unitOfWork.Materials.AddAsync(material);
            await _unitOfWork.Materials.SaveChangesAsync();

            return new CreateMaterialResponse
            {
                Id = material.Id,
                Code = material.Code,
                Name = material.Name,
                MaterialType = material.MaterialType,
                Specification = material.Specification,
                LengthMm = material.LengthMm,
                WidthMm = material.WidthMm,
                UnitOfMeasure = material.UnitOfMeasure
            };
        }
    }
}
