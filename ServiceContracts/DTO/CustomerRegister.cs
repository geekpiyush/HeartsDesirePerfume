using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    public class CustomerRegister
    {
        [Required(ErrorMessage ="Customer name is required")]
        public string? CustomerName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [RegularExpression("^[0-9]*$",ErrorMessage ="Phone should contains numbers only")]
        [DataType(DataType.PhoneNumber)]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "Password name is required")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Confirm password is required")]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }
    }
}
