using Restaurant.Entity.Enums;

namespace Restaurant.MVC.Areas.Manager.Models.ViewModels
{
    public class ReservationUpdateVM
    {
        public int Id { get; set; }
        public DateTime ReservationDate { get; set; }
        public int? TableOfRestaurantId { get; set; }
        public int? CustomerId { get; set; }

        public string? Description { get; set; }
        public ReservationStatus ReservationStatus { get; set; }


    }
}
