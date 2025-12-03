using InventoryService.Application.Features.Materials.Queries.GetMaterialById;

namespace InventoryService.UnitTests.Validators.Materials
{
    public class GetMaterialByIdQueryValidatorTests
    {
        [Fact]
        public void Validator_Fails_WhenIdIsZeroOrNegative()
        {
            var validator = new GetMaterialByIdQueryValidator();

            var query1 = new GetMaterialByIdQuery(0);
            var result1 = validator.Validate(query1);
            Assert.False(result1.IsValid);
            Assert.Contains(result1.Errors, e => e.PropertyName == "Id");

            var query2 = new GetMaterialByIdQuery(-5);
            var result2 = validator.Validate(query2);
            Assert.False(result2.IsValid);
        }

        [Fact]
        public void Validator_Passes_WhenIdIsPositive()
        {
            var validator = new GetMaterialByIdQueryValidator();

            var query = new GetMaterialByIdQuery(1);
            var result = validator.Validate(query);
            Assert.True(result.IsValid);
        }

    }
}
