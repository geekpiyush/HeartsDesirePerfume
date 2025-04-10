﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    public class CustomerLogin
    {
        [Required(ErrorMessage ="Email can't be blank")]
        [EmailAddress(ErrorMessage ="Email should be in proper email address format")]
        public string? Email { get; set; }

        [Required(ErrorMessage ="Password can't be blank")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        public bool? KeepMeSignedin { get; set; }
    }
}
