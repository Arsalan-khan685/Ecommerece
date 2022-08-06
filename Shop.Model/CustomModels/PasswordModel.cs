using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Model.CustomModels
{
    public class PasswordModel
    {
        public int userKey { get; set; }
        [Required(ErrorMessage ="*Old Password is required")]
        public string OldPassword { get; set; }
        [Required(ErrorMessage = "*New Password is required")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "*Confirm Password please")]
        public string ConfirmPassword { get; set; }
    }
}
