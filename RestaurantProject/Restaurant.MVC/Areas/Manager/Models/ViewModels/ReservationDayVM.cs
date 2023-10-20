using Restaurant.Entity.Enums;

namespace Restaurant.MVC.Areas.Manager.Models.ViewModels
{
    public class ReservationDayVM
    {
        public int ReservationId { get; set; }
        public string TableName { get; set; }
        public string CustomerName { get; set; }
        public DateTime ReservationDate { get; set; }
        public ReservationStatus ReservationStatus { get; set; }
        public string Description { get; set; }
    }
}
