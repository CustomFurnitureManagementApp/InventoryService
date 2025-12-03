using AutoMapper;
using InventoryService.Application.Common;
using InventoryService.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace InventoryService.Application.Features.Materials.Queries.GetMaterialById
{
    public class GetMaterialByIdQueryHandler(
        IUnitOfWork unitOfWork,
        ILogger<GetMaterialByIdQueryHandler> logger,
        IMapper mapper) :
        IRequestHandler<GetMaterialByIdQuery, Result<GetMaterialByIdResponse>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ILogger<GetMaterialByIdQueryHandler> _logger = logger;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<GetMaterialByIdResponse>> Handle(GetMaterialByIdQuery request, CancellationToken cancellationToken)
        {
            var materialEntity = await _unitOfWork.Materials.GetByIdAsync(request.Id);
            if (materialEntity == null)
            {
                _logger.LogWarning("Handler: The material with id {MaterialId} was not found.", request.Id);
                return Result<GetMaterialByIdResponse>.Failure(ErrorMessages.MaterialNotFound);
            }

            var materialDto = _mapper.Map<GetMaterialByIdResponse>(materialEntity);

            return Result<GetMaterialByIdResponse>.Success(materialDto);
        }
    }
}
