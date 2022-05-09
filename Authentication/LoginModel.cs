using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace aspnetApi.Authentication
{
    public class LoginModel
    {
        [Required(ErrorMessage = "User name is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]

        public string Password { get; set; }

    }
}