using Restaurant.Entity.Enums;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.Entity.ViewModels
{
    public class TableOfRestaurantVM
    {

        
        public string TableName { get; set; }
        
        public string TableLocation { get; set; }

        public int TableCapacity { get; set; }
        public ReservationStatus Status { get; set; }
        public int EmployeeId { get; set; }
        //public ReservationStatus ReservationStatus { get; set; } // rezervasyon durumu şuanlık başka bölümde ayarlanacak sonra burada düzenleme yapılacak
    }
}
