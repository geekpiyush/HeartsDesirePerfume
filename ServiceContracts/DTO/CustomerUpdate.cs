using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    public class CustomerUpdate
    {
        [Required(ErrorMessage ="ID is mandatory for update password")]
        public string EmailID { get; set; }

        [Required(ErrorMessage = "Password name is required")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Confirm password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and confirm password do not match")]
        public string? ConfirmPassword { get; set; }

    }
}
