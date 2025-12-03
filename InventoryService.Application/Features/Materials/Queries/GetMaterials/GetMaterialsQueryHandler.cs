using AutoMapper;
using InventoryService.Application.Common;
using InventoryService.Application.Features.Materials.Queries.GetMaterialById;
using InventoryService.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace InventoryService.Application.Features.Materials.Queries.GetMaterials
{
    public class GetMaterialsQueryHandler(
        IUnitOfWork unitOfWork, 
        ILogger<GetMaterialByIdQueryHandler> logger, 
        IMapper mapper) : 
        IRequestHandler<GetMaterialsQuery, Result<IReadOnlyList<GetMaterialsResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ILogger<GetMaterialByIdQueryHandler> _logger = logger;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<IReadOnlyList<GetMaterialsResponse>>> Handle(GetMaterialsQuery request, CancellationToken cancellationToken)
        {
            var materialEntities = await _unitOfWork.Materials.ListAsync();
            if (materialEntities == null)
            {
                _logger.LogWarning("Handler: There are no materials in the database");
                return Result<IReadOnlyList<GetMaterialsResponse>>.Failure(ErrorMessages.NoMaterials);
            }
            var materials = _mapper.Map<List<GetMaterialsResponse>>(materialEntities);

            return Result<IReadOnlyList<GetMaterialsResponse>>.Success(materials);
        }
    }
}
