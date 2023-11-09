using System.ComponentModel.DataAnnotations;

namespace Restaurant.MVC.Models.ViewModels
{
    public class SecurityProfileVM
    {
        //[Required(ErrorMessage = "Boş bırakılamaz")]
        public string PasswordNow { get; set; }
        //[Required(ErrorMessage = "Boş bırakılamaz")]
        public string PasswordHash { get; set; }
        //[Required(ErrorMessage = "Boş bırakılamaz")]
        //[Compare("PasswordHash", ErrorMessage = "Şifreler uyuşmuyor")]
        public string PasswordConfirmed { get; set; }
    }
}
