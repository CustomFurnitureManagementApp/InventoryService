using AutoMapper;
using InventoryService.Application.Common;
using InventoryService.Application.Features.Materials.Queries.GetMaterialById;
using InventoryService.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace InventoryService.Application.Features.Materials.Commands.CreateMaterial
{
    public class CreateMaterialCommandHandler(
        IUnitOfWork unitOfWork,
        ILogger<GetMaterialByIdQueryHandler> logger,
        IMapper mapper) :
        IRequestHandler<CreateMaterialCommand, Result<CreateMaterialResponse>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ILogger<GetMaterialByIdQueryHandler> _logger = logger;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<CreateMaterialResponse>> Handle(CreateMaterialCommand request, CancellationToken cancellationToken)
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

            var response = _mapper.Map<CreateMaterialResponse>(material);
            return Result<CreateMaterialResponse>.Success(response);
        }
    }
}
