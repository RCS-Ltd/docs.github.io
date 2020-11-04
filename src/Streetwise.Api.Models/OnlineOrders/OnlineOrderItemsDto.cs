namespace Streetwise.Api.Models
{
    public class OnlineOrderItemsDto : BaseIntIdModel
    {
        /// <summary>
        /// The order number field in OnlineOrders   NOP order GUID
        /// REQUIRED
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// Streetwise Product Code
        /// REQUIRED  Streetwise ProdCode
        /// </summary>
        public string ProductCode { get; set; }
        
        /// <summary>
        /// Qty requested in the order row
        /// REQUIRED
        /// </summary>
        public int QtyRequired { get; set; }
                        
        /// <summary>
        /// Price the customer paid
        /// REQUIRED
        /// </summary>
        public decimal PurchasePrice { get; set; }
        
        /// <summary>
        /// The ID of the order row item, in relation to sending store
        /// REQUIRED
        /// </summary>
        public string OrderRowId { get; set; }
        
        /// <summary>
        /// The price it would normally sell for
        /// REQUIRED
        /// </summary>
        public decimal StandardSellingPrice { get; set; }
        
        /// <summary>
        /// The promotion used on this order item
        /// </summary>
        public string PromotionId { get; set; }
    }
}
