namespace InventoryService.Domain.Enums
{
	public enum TransactionType
	{
		Receipt = 0,
		Issue = 1,
		Adjustment = 2,
		ProductionConsume = 3,
		ProductionProduce = 4,
		Transfer = 5
	}
}
