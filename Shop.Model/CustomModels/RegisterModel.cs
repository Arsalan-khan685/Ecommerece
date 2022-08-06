using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Model.CustomModels
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "*Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "*Email ID is required")]
        public string EmailID { get; set; }
        [Required(ErrorMessage ="*Password is requiured")]
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage ="*Mobile is required")]
        public string MobileNo { get; set; }
    }
}
