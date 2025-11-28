namespace InventoryService.Domain.Enums
{
    public enum StockTransactionType
    {
        PurchaseIn = 1,
        ProductionOut = 2,
        AdjustmentIn = 3,
        AdjustmentOut = 4,
        Reservation = 5,
        ReservationRelease = 6
    }

}
