namespace Streetwise.Api.Models
{
    public enum OrderStatus
    {        
        New = 1,
        InPicking = 2,
        PickingComplete = 3,
        ToTransfer = 4,
        Transfered = 5,
        PriceConfirmed = 6,
        Completed = 7,
        ToDelete = 8,
        Deleted = 9,
        ToRefund = 10,
        Refunded = 11
    }

    public enum OrderItemStatus
    {        
        ToPick = 1,
        InPicking = 2,
        PickingComplete = 3,
        ToTransfer = 4,
        Transfered = 5,
        PickConfirmed = 6,
        Complete = 7,
        ToDelete = 8,
        Deleted = 9,
        ToRefund = 10,
        Refunded = 11
    }
}
