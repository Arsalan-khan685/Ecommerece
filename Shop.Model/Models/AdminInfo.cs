using System;
using System.Collections.Generic;

#nullable disable

namespace Shop.Model.Models
{
    public partial class AdminInfo
    {
        public int AdminId { get; set; }
        public string AdminName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string LastLogin { get; set; }
        public string CreatedOn { get; set; }
        public string UpdateOn { get; set; }
    }
}
