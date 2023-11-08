using System.ComponentModel.DataAnnotations;

namespace Restaurant.MVC.Models.ViewModels
{
    public class ProfileVM
    {
    
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        //[Required]
        //public string PasswordHash { get; set; }
        //[Required(ErrorMessage = "Boş bırakılamaz")]
        //[Compare("PasswordHash")]
        //public string PasswordConfirmed { get; set; }
    }
}
