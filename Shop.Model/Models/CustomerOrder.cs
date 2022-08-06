using System;
using System.Collections.Generic;

#nullable disable

namespace Shop.Model.Models
{
    public partial class CustomerOrder
    {
        public int CustomerOrderId { get; set; }
        public int? CustomerId { get; set; }
        public string OrderId { get; set; }
        public string PaymentMode { get; set; }
        public string ShippingAddress { get; set; }
        public int? ShippingCharges { get; set; }
        public int? Total { get; set; }
        public int? Subtotal { get; set; }
        public string ShippingStatus { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
