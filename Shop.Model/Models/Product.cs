using System;
using System.Collections.Generic;

#nullable disable

namespace Shop.Model.Models
{
    public partial class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int? Price { get; set; }
        public int? CategoryId { get; set; }
        public string ImageUrl { get; set; }
        public int? Stock { get; set; }
    }
}
