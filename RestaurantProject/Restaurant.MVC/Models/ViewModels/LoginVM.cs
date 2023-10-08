using System.ComponentModel.DataAnnotations;

namespace Restaurant.MVC.Models.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage =("Boş bırakılamaz"))]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = ("Boş bırakılamaz"))]
        public string Password { get; set; }
    }
}
