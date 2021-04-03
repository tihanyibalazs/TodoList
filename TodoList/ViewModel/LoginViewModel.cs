﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TodoList.ViewModel
{
    public class LoginViewModel
    {
        [DisplayName("Username: ")]
        [Required]
        public string UserName { get; set; }

        [Required]
        [DisplayName("Password: ")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
