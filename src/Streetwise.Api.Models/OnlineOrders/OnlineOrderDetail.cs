using System.Collections.Generic;

namespace Streetwise.Api.Models
{
    /// <summary>
    /// Basket / Online order information model
    /// </summary>
    public class OnlineOrderDetail
    {
        /// <summary>
        /// OnlineOrder object
        /// </summary>
        public OnlineOrderDto OrderDetails { get; set; }
        
        /// <summary>
        /// List of OnlineOrderItems
        /// </summary>
        public ICollection<OnlineOrderItemsDto> OrderItems { get; set; }

        /// <summary>
        /// Delivery address for this order
        /// </summary>
        public OnlineOrderDeliveryAddressDto DeliveryAddress { get; set; }

    }
}
