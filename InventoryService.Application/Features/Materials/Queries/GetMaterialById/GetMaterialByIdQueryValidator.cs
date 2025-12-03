using FluentValidation;

namespace InventoryService.Application.Features.Materials.Queries.GetMaterialById
{
    public class GetMaterialByIdQueryValidator : AbstractValidator<GetMaterialByIdQuery>
    {
        public GetMaterialByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Material Id must be greater than 0.");
        }
    }
}
