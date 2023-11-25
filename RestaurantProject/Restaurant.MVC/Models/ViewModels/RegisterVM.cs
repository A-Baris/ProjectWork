using Restaurant.Entity.Enums;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.MVC.Models.ViewModels
{
    public class RegisterVM
    {
        [Display(Name = "Müşteri Ad")]
        public string CustomerName { get; set; }
        [Display(Name = "Müşteri Soyad")]
        public string CustomerSurname { get; set; }

        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; }
        [Display(Name = "Şifre")]
        public string Password { get; set; }
        [Display(Name = "Şifre Tekrar")]
        public string PasswordConfirmed { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Cep No")]
        
        public string PhoneNumber { get; set; }
        
        public bool UserRight { get; set; }
    }
}
