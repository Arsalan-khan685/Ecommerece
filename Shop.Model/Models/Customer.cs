using System;
using System.Collections.Generic;

#nullable disable

namespace Shop.Model.Models
{
    public partial class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public DateTime? LastLogin { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string Password { get; set; }
    }
}
