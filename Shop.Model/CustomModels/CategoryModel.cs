using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Model.CustomModels
{
    public class CategoryModel
    {
        public int CategoryID { get; set; }
        [Required(ErrorMessage = "*Category Name is required")]
        public string CategoryName { get; set; }
    }
}
