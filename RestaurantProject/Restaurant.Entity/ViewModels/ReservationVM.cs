using System.ComponentModel.DataAnnotations;

namespace Restaurant.MVC.Areas.Manager.Models.ViewModels
{
    public class ReservationVM
    {
        public int? TableOfRestaurantId { get; set; }
        [Display(Name = "Rezervasyon Durum")]
        public string ReservationStatus { get; set; }
        public string? CustomerId { get; set; }
        [Display(Name = "Açıklama")]
        public string? Description { get; set; }
        [Display(Name = "Rezervasyon Tarih")]
        public DateTime ReservationDate { get; set; }
        [Display(Name = "Misafir Sayısı")]
        public int GuestNumber { get; set; }
    }
}
