using Restaurant.Entity.Enums;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.MVC.Areas.Manager.Models.ViewModels
{
    public class TableOfRestaurantVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Bu alan boş bırakılmaz")]
        public string Location { get; set; }
        
        [Required(ErrorMessage = "Bu alan boş bırakılamaz")]
        public string TableName { get; set; }
        [Required(ErrorMessage = "Lütfen boş bırakmayınız")]

        [Range(1, int.MaxValue, ErrorMessage = "Geçerli bir kapasite değeri giriniz.")]
        public int Capacity { get; set; }
        public ReservationStatus Status { get; set; }

        [Required(ErrorMessage = "Lütfen boş bırakmayınız")]
        [Range(1, int.MaxValue, ErrorMessage = "Lütfen seçim yapınız")]
        public int EmployeId { get; set; }
        //public ReservationStatus ReservationStatus { get; set; } // rezervasyon durumu şuanlık başka bölümde ayarlanacak sonra burada düzenleme yapılacak
    }
}
