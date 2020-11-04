using System;

namespace Streetwise.Api.Models
{
    public class OnlineOrderDto : BaseIntIdModel
    {        
        /// <summary>
        /// The order number / NOP Order GUID
        /// REQUIRED
        /// </summary>
        public string OrderNo { get; set; }
        
        /// <summary>
        /// The store members code
        /// </summary>
        public int MemberCode { get; set; }
        
        /// <summary>
        /// The date the order was created
        /// REQUIRED
        /// </summary>
        public DateTime OrderDate { get; set; }
        
        /// <summary>
        /// Customer GUID in nop   or ID to string in others
        /// REQUIRED
        /// </summary>
        public string CustomerGuid { get; set; }
        
        /// <summary>
        /// Ability to extend with notes or other messages
        /// use the field for delivery notes too
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// If customer is actually a member of staff
        /// REQUIRED
        /// </summary>
        public bool IsStaffMember { get; set; }

        /// <summary>
        /// Delivery charge, if none set 0
        /// REQUIRED
        /// </summary>
        public decimal DeliveryCharge { get; set; }

        /// <summary>
        /// The total and final value for the order. i.e Amount customer has paid
        /// REQUIRED
        /// </summary>
        public decimal TotalOrderValue { get; set; }

        /// <summary>
        /// Numeric order number for display reasons
        /// REQUIRED
        /// </summary>
        public int OrderNumber { get; set; }

        /// <summary>
        /// Location that the order has been created for delivery from
        /// REQUIRED
        /// </summary>
        public string LocationCode { get; set; }

        /// <summary>
        /// This is for the special code, that is then turned into a barcode
        /// for the delivery slip / sticker.   The barcode points to the address
        /// when scanned by compatable devices
        /// </summary>
        public string DeliveryReferenceCode { get; set; }

        /// <summary>
        /// true if age restricted products in order
        /// Required
        /// </summary>
        public bool IsAgeVerificationRequired { get; set; }

        /// <summary>
        /// true if online verification has been carried out
        /// Required
        /// </summary>
        public bool HasAgeBeenVerified { get; set; }

        /// <summary>
        /// Staff Discount Value
        /// Required
        /// </summary>
        public decimal StaffDiscount { get; set; }

        /// <summary>
        /// Discount Coupon Value
        /// Required
        /// </summary>
        public decimal CouponValue { get; set; }

        /// <summary>
        /// Date order was marked as delivered
        /// </summary>
        public DateTime? DeliveryDate { get; set; }
    }
}
