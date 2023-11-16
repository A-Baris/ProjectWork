using System.ComponentModel.DataAnnotations;

namespace Restaurant.MVC.Models.ViewModels
{
    public class ProfileVM
    {
    
        [Required]
        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Cep No")]
        public string PhoneNumber { get; set; }
        [Required]
        [Display(Name = "Müşteri Ad")]
        public string CustomerName { get;set; }
        [Required]
        [Display(Name = "Mutfak Soyad")]
        public string CustomerSurname { get;set; }
    }
}
