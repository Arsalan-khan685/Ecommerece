using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Model.CustomModels
{
    public class CartModel
    {
        public int UserID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public int Quantity { get; set; }
        public int price { get; set; }
        public int AvailableStock { get; set; }
        public int ShippingCharges { get; set; }
        public int SubTotal { get; set; }
        public int Total { get; set; }
        public string ShippingAddress { get; set; }
        public string PaymentMode { get; set; }
    }
}
