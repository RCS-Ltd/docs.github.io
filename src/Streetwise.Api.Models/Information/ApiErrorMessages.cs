using System.Collections.Generic;
using System.Linq;

namespace Streetwise.Api.Models
{
    public static class ApiErrorMessages
    {
        public const string ExpiredLogin = "Token expired, please login again";
        public const string AccessDenied = "Access Denied";
        public const string MissingClientId = "ClientId cannot be null";
        public const string MissingClientSecret = "Client Secret cannot be null";
        public const string MissingAccessToken = "You must provide an access token";
        public const string TransportError = "Transport error with status code";
        public const string NoValidContent = "You must provide content in the data field";
        public const string ValidationError = "Please check validation errors for details";
        public const string GeneralException = "An exception happened, we have logged the details";
        public const string OrderItemNotExist = "Order item with OrderRowId {{ID}} does not exist";
        public const string OrderItemCannotBeEdited = "Order item with OrderRowId {{ID}} cannot be edited due to status";
        public const string OrderCannotBeFound = "Order with ID {{ID}} cannot be found for updating";
        public const string OrderCannotBeEdited = "Order with ID {{ID}} cannot be edited due to current status";
        public const string InvalidOrMissingOrder = "Order is invalid or null";
        public const string AddressMissing = "The delivery address is missing";
        public const string NoOrderItemsPresent = "An order must have order items";
        public const string OrderValueZero = "An order must have a total value more than zero";
        public const string OrderTotalMisMatch = "Order total, does not match with order items.  item.Qty*item.PurchasePrice = RowTotal.";
        public const string OrderCannotBeRefunded = "Order {{ID}} cannot be refunded as it is not yet completed.";
        public const string OrderItemCannotBeRefunded = "Order item {{ID}} cannot be refunded as it is not yet completed";

        /// <summary>
        /// Use this to compare error message, against existing messages
        /// </summary>
        /// <returns></returns>
        public static List<string> GetAll()
        {
            return (from d in typeof(ApiErrorMessages).GetFields() select d.GetRawConstantValue().ToString()).ToList();
        }
    }
}
