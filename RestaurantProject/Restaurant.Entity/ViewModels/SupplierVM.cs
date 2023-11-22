using System.ComponentModel.DataAnnotations;

namespace Restaurant.MVC.Areas.Manager.Models.ViewModels
{
    public class SupplierVM
    {
        [Display(Name = "Şirket Adı")]

        public string CompanyName { get; set; }
        [Display(Name = "Adres")]

        public string Adress { get; set; }
        [Display(Name = "Cep No")]
        public string Phone { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Yetkili Kişi")]
        public string ContactPerson { get; set; }
        [Display(Name = "Görevi")]
        public string Title { get; set; }


        [Display(Name = "Açıklama")]
        public string? Description { get; set; }
    }
}
