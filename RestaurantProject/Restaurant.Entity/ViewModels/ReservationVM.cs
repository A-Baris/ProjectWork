namespace Restaurant.MVC.Areas.Manager.Models.ViewModels
{
    public class ReservationVM
    {
        public int? TableOfRestaurantId { get; set; }
     
        public string ReservationStatus { get; set; }
        public string? CustomerId { get; set; }
        public string? Description { get; set; }
        public DateTime ReservationDate { get; set; }
    }
}
