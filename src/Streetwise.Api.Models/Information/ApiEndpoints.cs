namespace Streetwise.Api.Models
{
    public class ApiEndpoints
    {
        public const string PostOnlineOrderDetail = "api/OnlineOrderDetail/Create";
        public const string FinalOrderConfirm = "api/OnlineOrderDetail/FinalOrderConfirm";
        public const string CancelOrder = "api/OnlineOrderDetail/CancelOrder";
        public const string CancelOrderItems = "api/OnlineOrderDetail/CancelOrderItems";
        public const string RefundOrder = "api/OnlineOrderDetail/RefundOrder";
        public const string RefundOrderItem = "api/OnlineOrderDetail/RefundOrderItem";

        // Exporting Endpoints.  can be used with Streetwise.Api.Connect.ApiAccessService.GetData
        public const string GetProductChanges = "api/ExportProduct/GetProductChanges";
        public const string GetAllProductsExport = "api/ExportProduct/GetAll";
        public const string ExportCategories = "api/ExportCategory/GetAll";
        public const string ExportPrices = "api/ExportPrice/GetAll";
        public const string ExportPromotions = "api/ExportPromotion/GetForLocation";
        public const string ExportStock = "api/ExportStock/GetAll";
    }
}
