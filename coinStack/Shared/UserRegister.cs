using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coinStack.Shared
{
    public class UserRegister
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required, StringLength(16, ErrorMessage = "Your Username is too long (16 characters max)")]
        public string Username { get; set; }
        [Required, StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "The passwords do not match.")]
        public string ConfirmPassword { get; set; }

    }
}
