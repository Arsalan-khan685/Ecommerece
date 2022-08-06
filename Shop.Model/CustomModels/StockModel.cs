using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Model.CustomModels
{
    public class StockModel
    {
        public int StockID { get; set; }
        public string ProductName { get; set; }
        public int Stock { get; set; }
        [Required(ErrorMessage="*Product Stock is required")]
        public int NewStock { get; set; }
        public string Category_Name { get; set; }
    }
}
