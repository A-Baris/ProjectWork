using Restaurant.Entity.Enums;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.Entity.ViewModels
{
    public class TableOfRestaurantVM
    {

        [Display(Name = "Masa Adı")]
        public string TableName { get; set; }
        [Display(Name = "Masa Konum")]
        public string TableLocation { get; set; }
        [Display(Name = "Masa Kapasite")]
        public int TableCapacity { get; set; }
        [Display(Name = "Rezervasyon Durum")]
        public ReservationStatus Status { get; set; }
        public int EmployeeId { get; set; }
  
    }
}
