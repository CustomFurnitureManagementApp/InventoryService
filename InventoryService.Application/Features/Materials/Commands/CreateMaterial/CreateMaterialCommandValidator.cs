using FluentValidation;

namespace InventoryService.Application.Features.Materials.Commands.CreateMaterial
{
    public class CreateMaterialCommandValidator : AbstractValidator<CreateMaterialCommand>
    {
        public CreateMaterialCommandValidator()
        {
            // Code - required, max 50 chars
            RuleFor(x => x.Code)
                .MaximumLength(50).WithMessage("Code cannot exceed 50 characters");

            // Name - required, max 100 chars
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters");

            // MaterialType - must be a valid enum
            RuleFor(x => x.MaterialType)
                .IsInEnum().WithMessage("MaterialType must be a valid value");

            // UnitOfMeasure - must be a valid enum
            RuleFor(x => x.UnitOfMeasure)
                .IsInEnum().WithMessage("UnitOfMeasure must be a valid value");

            // Specification - optional, max 50 chars
            RuleFor(x => x.Specification)
                .MaximumLength(50).WithMessage("Specification cannot exceed 50 characters")
                .When(x => !string.IsNullOrEmpty(x.Specification));
        }
    }
}
