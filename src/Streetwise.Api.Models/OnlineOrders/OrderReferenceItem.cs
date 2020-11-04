using System;
using System.Collections.Generic;
using System.Text;

namespace Streetwise.Api.Models
{
    public class OrderReferenceItem
    {
       public int QtyRequired { get; set; }
       public decimal PurchasePrice { get; set; }
       public string OrderRowId { get; set; }
    }
}
