---
# Feel free to add content and custom Front Matter to this file.
# To modify the layout, see https://jekyllrb.com/docs/themes/#overriding-theme-defaults

layout: default
---
## Using the provided library Streetwise.Api.Connect

The library is a .net standard 2.0 library.  and should work in almost any platform that runs .net and supports .net standard.


### Available Methods

```cs
	// Your clientid, your token, the store url ending with /slash
	_authService.GetLogin(string clientId, string clientSecret, string apiUrl)

	// example
	_authService.GetLogin("hudnoewihrnl.lcidjoukjfhrwrug", "asjhajshajshjaskjhuwjb7213tevdyuy7ewru", "https:127.0.0.1:8858/")

	public async Task<ApiResponse> GetData(RequestModel requestModel, string apiUrl, string endpoint)

	public async Task<ApiResponse> SendData(RequestModel requestModel, string apiUrl, string endpoint)
```

## Data Specific Models

```cs
  // Details of error messages
  Streetwise.Api.Connect.Models.ApiErrorMessages
  // example
  var error = Streetwise.Api.Connect.Models.ApiErrorMessages.ExpiredLogin // output => "Token expired, please login again"

  // these are the standard messages we send.
  // we provide a list of the message incase you want to check if error.contains(ApiErrorMessages)
  Streetwise.Api.Connect.Models.ApiErrorMessages.GetAll(); // returns list<string>() 

  //For the GetData and SendData endpoints. The urls are from ApiEndpoints
  // rather than the ApiAuthEndpoints, here are the details

  namespace Streetwise.Api.Models
  {
	public class ApiEndpoints
	{
		public const string PostOnlineOrderDetail = "api/OnlineOrderDetail/Create";
		public const string FinalOrderConfirmWms = "api/OnlineOrderDetail/FinalOrderConfirmWms";
		public const string CancelOrder = "api/OnlineOrderDetail/CancelOrder";
		public const string CancelOrderItems = "api/OnlineOrderDetail/CancelOrderItems";
		public const string RefundOrder = "api/OnlineOrderDetail/RefundOrder";
		public const string RefundOrderItem = "api/OnlineOrderDetail/RefundOrderItem";

		public const string GetOrders = "api/OnlineOrderDetail/Search";
		public const string GetOrderDetails = "api/OnlineOrderDetail/ViewOrder";
		public const string GetOrderSearchModel = "api/OnlineOrderDetail/SearchModel";
	}
  }

  namespace Streetwise.Api.Models
{
	public enum OrderStatus
	{        
		New = 1,   //Order Received
		InPicking = 2, // transfered to WMS
		PickingComplete = 3, // picking done
		ToTransfer = 4, // waiting external notification
		Transfered = 5, // external notification sent ( Shipment received from WMS )
		PriceConfirmed = 6, // external notification accepted ( Shipment sent to Online store ) 
		Completed = 7,
		ToDelete = 8,
		Deleted = 9,
		ToRefund = 10,
		Refunded = 11
	}

	public enum OrderItemStatus
	{
		InsufficientStock = 0,
		ToPick = 1,  //new no actions taken
		InPicking = 2, // with warehouse or store
		PickingComplete = 3, // ready for dispatch
		ToTransfer = 4, // waiting for enternal notification
		Transfered = 5, // xternal notification sent ( Shipment received from WMS )
		PickConfirmed = 6, // external notification accepted ( Shipment sent to Online store ) 
		Complete = 7,
		ToDelete = 8,
		Deleted = 9,
		ToRefund = 10,
		Refunded = 11
	}
}
  
```

## Models

#### Login Request

```cs
	public class LoginModel
	{
		public string ClientId { get; set; }
		public string ClientSecret { get; set; }
	}

	JSON: {
	"clientId" : "Your CLient Id", 
	"clientSecret" : "Your Client Secret" 
	}
```

#### Login Response
We return this model when you call the Login endpoint
this is the type returned on good or bad login

This is also the response from RefreshLogin
```cs
	public class LoginResponse
	{
		/// <summary>
		/// Your access token for futher authentification
		/// </summary>
		public string AccessToken { get; set; }

		/// <summary>
		/// The datetime your token expires ( usually now + 10 mins ) 
		/// local server time
		/// </summary>
		public DateTime Expires { get; set; }

		/// <summary>
		/// Error message, from ApiErrorMessages object
		/// </summary>
		public string ErrorMessage { get; set; }

		/// <summary>
		/// Check if true login success, false means check the error message
		/// </summary>
		public bool IsSuccess { get; set; }
	}

	JSON: {
	   "accessToken" : "",
	   "expires" : "",
	   "errorMessage" : "",
	   "isSuccess" : "false"
	}
```

#### Request Model

For request, the library uses a generic handler.

```cs
	public class BaseModel
	{
		/// <summary>
		/// A valid acccess token, used to authenticate your request
		/// </summary>
		public string AccessToken { get; set; }
	}

	public class RequestModel : BaseModel
	{
		/// <summary>
		/// Serialize your data and add the output into the Data property
		/// </summary>
		public string Data { get; set; }
	}

	JSON: {
	"accessToken" : "Your access token",
		"data" : "Your object convert to json string object"
	}
```

#### Response Model
And We also always respond with the same model.  Check status of success
if you get a false,  You can check the ValidationErrors for property based issues
and you can check the ErrorMessage property for other message types.

The Errors in ErrorMessage are detailed in ApiErrorMessages model.

```cs
	public class ApiResponse
	{
		/// <summary>
		/// True if all ok, false if errorMessage needs to be checked
		/// </summary>
		public bool Success { get; set; }

		/// <summary>
		/// An error message, if Success = false
		/// from ApiErrorMessages object
		/// </summary>
		public string ErrorMessage { get; set; }

	/// <summary>
		/// List errors from property validation
		/// </summary>
		public List<ValidationError> ValidationErrors { get; set; }

		/// <summary>
		/// Object of a single item.  be that of class, string int  etc
		/// used on getdata
		/// </summary>
		public object Item { get; set; }

		/// <summary>
		/// List of items to send. i.e Orders  or string messages
		/// used on getdata
		/// </summary>
		public List<object> Items { get; set; }
	}
 
	JSON: {
	   "success" : "false",
	   "errorMessage" : "",
	   "validationErrors" : [],
	   "item" : "Result",
	   "items" : ["result", "result"]
	}

	public class ValidationError
	{
	/// <summary>
	/// The name of the property.
	/// </summary>
	public string PropertyName { get; set; }

	/// <summary>
	/// The error message
	/// </summary>
	public string ErrorMessage { get; set; }

	/// <summary>
	/// The property value that caused the failure.
	/// </summary>
	public object AttemptedValue { get; set; }
   }

   JSON : {
	"propertyName" : "",
	"errorMessage" : "",
	"attemptedValue: ""
   }
```



## The Data Models.  Streetwise.Api.Models

In the request model, you will return an object or many objects in the item or items fields
as it stands we are covering order information.  Below is the model you will use and set to the item property
I have also included a breakdown of the types used in that model for clarity.

#### Streetwise.Api.Models.OnlineOrderDetail
```cs
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

	JSON: {
	"orderDetails" : OnlineOrderDto,
		"orderItems" : [OnlineOrderItemsDto, OnlineOrderItemsDto, OnlineOrderItemsDto],
	"deliveryAddress" : OnlineOrderDeliveryAddressDto
	}
```

#### Streetwise.Api.Models.OnlineOrderDto
```cs
		/// <summary>
		/// The order number
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

		public string StreetwiseOrderNumber { get; set; }

	JSON : {
		"orderNo" : "",
		"memberCode" : 0,
		"orderDate": "2020/09/09 12:24:36",
		"customerGuid" : "",
		"notes" : "",
		"isStaffMember" : false,
		"deliveryCharge" : 0,
		"totalOrderValue" : 0,
		"orderNumber" : 0,
		"locationCode" : "0024"
		"deliveryReferenceCode" : "",
		"isAgeVerificationRequired" : false,
		"hasAgeBeenVerified" : false,
		"staffDiscount" : 0,
		"couponValue" : 0,
		"deliveryDate" : null,
		"streetwiseOrderNumber" : null
	}
``` 

#### Streetwise.Api.Models.OnlineOrderItemsDto
```cs
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

		public string StreetwiseOrderNumber { get; set; }

	JSON : {
		"orderNo" : "",
		"productCode" : "",
		"qtyRequired" : "",
		"purchasePrice" : "",
		"orderRowId" : "",
		"standardSellingPrice" : 0,
		"promotionId" : "",
		"streetwiseOrderNumber" : null
	}
```

#### Streetwise.Api.Models.OnlineOrderDeliveryAddressDto

```cs
/// <summary>
		/// Gets or sets the first name
		/// REQUIRED
		/// </summary>
		public string FirstName { get; set; }

		/// <summary>
		/// Gets or sets the last name
		/// REQUIRED
		/// </summary>
		public string LastName { get; set; }

		/// <summary>
		/// Gets or sets the email
		/// if not null, must be valid email address
		/// </summary>
		public string Email { get; set; }

		/// <summary>
		/// Gets or sets the company
		/// </summary>
		public string Company { get; set; }
		
		/// <summary>
		/// Gets or sets the county
		/// </summary>
		public string County { get; set; }

		/// <summary>
		/// Gets or sets the city
		/// </summary>
		public string City { get; set; }

		/// <summary>
		/// Gets or sets the address 1
		/// REQUIRED
		/// </summary>
		public string Address1 { get; set; }

		/// <summary>
		/// Gets or sets the address 2
		/// </summary>
		public string Address2 { get; set; }

		/// <summary>
		/// Gets or sets the zip/postal code
		/// REQUIRED
		/// </summary>
		public string ZipPostalCode { get; set; }

		/// <summary>
		/// Gets or sets the country
		/// </summary>
		public string Country { get; set; }

		/// <summary>
		/// Gets or sets the phone number
		/// </summary>
		public string PhoneNumber { get; set; }      

	JSON : {
		"firstName" : "",
		"lastName" : "",
		"email" : "",
		"company" : "",
		"county" : "",
		"city" : "",
		"address1" : "",
		"address2" : "",
		"zipPostalCode" : "",
		"country" : "",
		"phoneNumber" : ""
	} 
```

## For new refund and cancellations

#### Streetwise.Api.Models.OrderReferenceHeader

```cs
	public class OrderReferenceHeader
	{
		public string OrderGuid { get; set; }
		public int OrderId { get; set; }
		public decimal DeliveryCharge { get; set; }
		public decimal TotalOrderValue { get; set; }
		public string StreetwiseOrderNumber { get; set; }

		/// <summary>
		/// Note that for refunds, the qty and price needs to be the amount and qty to refund
		/// and not what the new row will be equal to
		/// </summary>
		public List<OrderReferenceItem> Items { get; set; }
	}

	JSON : {
		"orderGuid" : "",
		"orderId: 0,
		"deliveryCharge" : 0,
		"totalOrderValue" : 0,
		"streetwiseOrderNumber" : null
		"items: [OrderReferenceItem, OrderReferenceItem, OrderReferenceItem]
	}
```

#### Streetwise.Api.Models.OrderReferenceItem

```cs
	public class OrderReferenceItem
	{
	   public int QtyRequired { get; set; }
	   public decimal PurchasePrice { get; set; }
	   public string OrderRowId { get; set; }
	}

	JSON : {
		"qtyRequired" : 0,
		"purchasePrice" : 0,
		"orderRowId" : ""   	
	}
```

## Refunds And Cancellations

Takes in a OrderReferenceHeader as the data.
The TotalOrderValue.  this should be including the delivery charge

1.  Can Cancel whole order.   Must contain order items
2.  Can Cancel Order item ( not partial )  Must include order header new total as it should be now, and delivery.
3.  Can refund whole order,  must contain order items
4.  Can Refund and partially refund a orderItem.   Must include order TotalOrderValue and delivery.

We Will and do check that the order total matches the order total we expect.  We do this by calculating
the valid rows for the order ( not cancelled or refunded )   and then check if the calculated value + delivery = TotalOrderValue.

If this fails, we dont save anything.    We also have the folowing rules:

### Cancellation status:

```cs
	var validOrderStatus = new List<int> { 
		(int)OrderStatus.New, 
		(int)OrderStatus.ToTransfer, 
		(int)OrderStatus.Transfered,
		(int)OrderStatus.PickingComplete
	};

	// set status allowed for cancel
	var validOrderItemStatus = new List<int>
	{
		(int)OrderItemStatus.ToPick,
		(int)OrderItemStatus.ToTransfer,
		(int)OrderItemStatus.Transfered,
		(int)OrderItemStatus.PickingComplete
	};
```

### Refund Status

Order and order items must be in a state of completed.

### Order Search

The search for orders requires `Streetwise.Api.Models.OrderSearchAndResults` to be passed in the data object
of the requestModel.   This model can be requested from the API using the GetData method, along with the `GetOrderSearchModel` endpoint.
The advantage of using the Search model from the API is that it will come pre-loaded with status types available for search.

You can of course, skip the fetching of the pre-loaded model and just use the model directly.

## Model
```cs

namespace Streetwise.Api.Models
{
    public class OrderSearchAndResults
    {
        /// <summary>
        /// The Results of the search
        /// </summary>
        public List<OrderSearchResult> Results { get; set; }

        /// <summary>
        /// List of available status
        /// </summary>
        public List<string> StatusValues { get; set; }

        /// <summary>
        /// List of status types to search for
        /// </summary>
        public List<string> SearchStatus { get; set; }

        /// <summary>
        /// Daterange search   Start date of range
        /// </summary>
        public DateTime? StartRange { get; set; }

        /// <summary>
        /// Date range search, end of search range
        /// </summary>
        public DateTime? EndRange { get; set; }

        /// <summary>
        /// Specific location to search for
        /// </summary>
        public string LocationCode { get; set; }
    }
}

namespace Streetwise.Api.Models
{
    public class OrderSearchResult
    {
        public int Id { get; set; }
        public decimal OrderTotal { get; set; }
        public string LocationCode { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public int OrderStatusCode { get; set; }
        public string OrderGuid { get; set; }
        public int OrderId { get; set; }
        public DateTime OrderDateUtc { get; set; }
        public int RowCount { get; set; }
        public string OrderStatus { get; set; }
        public string StreetwiseOrderId { get; set; }
    }
}

// JSON Object


    {
        "results": [{
			"id" : 0,
			"orderTotal": 14.99,
			"locationCode": '0012',
			"DeliveryDate" : '',
			"orderStatusCode: 7,
			"orderGuid": null,
			"orderId: 1234,
			"orderDateUtc": '',
			"rowCount: 3,
			"OrderStatus" : 'Complete',
			"streetwiseOrderId": '01234'
        }],
		"statusValues": [New,InPicking,PickingComplete,ToTransfer,Transfered,PriceConfirmed,Completed,ToDelete,Deleted,ToRefund,Refunded],
		"searchStatus": [Complete],
		"startRange: '',
		"endRange" : '',
		"locationCode: '0012'
    }

```

### Order Details



## Validation

Note that we sanitize all data coming into the API.
the general rule is this regex: 

```cs
@"[^\w\.@-]"
```

It allows letters, number and normal punctuation like spaces.   It is also extended to allow @ . and -
We do not warn or validate if the property contains an unwanted char, we just sanitize. It is up to the sender
to make sure that data is not going to loose context, by having invalid charactors.

It is also worth a mention that any email address fields, will be checked, and as such, if the email field
is not null then the value of that field must validate as an email address string. We will warn about this.

It would be best practice to log all failed responses from the API, in order to ensure that a checker can be created
on your side, in order to make sure that no orders have been missed.  If the order is corrected, You can re-post for re-validation.

So it would also be prundent to add in the facility to re-send / re-try an order notification.

### Exception / Error Messages

```cs
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
```
