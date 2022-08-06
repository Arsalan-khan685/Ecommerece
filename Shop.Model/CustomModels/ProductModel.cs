using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Model.CustomModels
{
    public class ProductModel
    {
        public int ProductID { get; set; }
        [Required(ErrorMessage="*Product Name is required")]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "*Product Price is required")]
        public int Price { get; set; }
        [Required(ErrorMessage = "*Product Stock is required")]
        public int Stock { get; set; }
        [Required(ErrorMessage = "*Product Category is required")]
        public int Category_ID { get; set; }
        public string Category_Name { get; set; }
        public string ImageUrl { get; set; }
        [Required(ErrorMessage = "*Kindly Upload Product Photo")]
        public string FileName { get; set; }
        public byte[] FileContent { get; set; }
        public bool CartFlag { get; set; }
    }
}
