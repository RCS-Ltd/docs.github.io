using System.Collections.Generic;

namespace Streetwise.Api.Models
{
    public class OrderReferenceHeader
    {
        public string OrderGuid { get; set; }
        public int OrderId { get; set; }
        public decimal DeliveryCharge { get; set; }
        public decimal TotalOrderValue { get; set; }


        /// <summary>
        /// Note that for refunds, the qty and price needs to be the amount and qty to refund
        /// and not what the new row will be equal to
        /// </summary>
        public List<OrderReferenceItem> Items { get; set; }
    }
}
