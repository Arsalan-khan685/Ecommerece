using System;
using System.Collections.Generic;

#nullable disable

namespace Shop.Model.Models
{
    public partial class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public string OrderId { get; set; }
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }
        public int? Price { get; set; }
        public int? SubTotal { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
