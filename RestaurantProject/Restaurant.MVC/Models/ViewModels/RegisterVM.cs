using System.ComponentModel.DataAnnotations;

namespace Restaurant.MVC.Models.ViewModels
{
    public class RegisterVM
    {
        [Required(ErrorMessage ="Boş bırakılamaz")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Boş bırakılamaz")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Boş bırakılamaz")]
        [Compare("Password")]
        public string PasswordConfirmed { get; set; }
        [Required(ErrorMessage = "Boş bırakılamaz")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Boş bırakılamaz")]
        [Phone]
        public string PhoneNumber { get; set; }
    }
}
