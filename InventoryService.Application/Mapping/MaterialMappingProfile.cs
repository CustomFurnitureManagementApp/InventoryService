using AutoMapper;
using InventoryService.Application.Features.Materials.Commands.CreateMaterial;
using InventoryService.Application.Features.Materials.Queries.GetMaterialById;
using InventoryService.Application.Features.Materials.Queries.GetMaterials;
using InventoryService.Domain.Entities.Master;

namespace InventoryService.Application.Mapping
{
    public class MaterialMappingProfile : Profile
    {
        public MaterialMappingProfile()
        {
            CreateMap<CreateMaterialCommand, Material>();
            CreateMap<Material, CreateMaterialResponse>();

            CreateMap<Material, GetMaterialByIdResponse>();

            CreateMap<Material, GetMaterialsResponse>();
        }
    }
}
