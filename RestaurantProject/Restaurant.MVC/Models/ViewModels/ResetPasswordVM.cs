using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.MVC.Models.ViewModels
{
    public class ResetPasswordVM
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        [DisplayName("Şifre")]
        public string Password { get; set; }
        [DisplayName("Şifre Tekrar")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
