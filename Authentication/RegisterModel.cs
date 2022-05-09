using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace aspnetApi.Authentication
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "User name is required")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Please enter valid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]

        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")] public string ConfirmPassword { get; set; }
    }
}