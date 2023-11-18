using Restaurant.Entity.Enums;

namespace Restaurant.MVC.Models.ViewModels
{
    public class CustomerReservationVM
    {
        public int Id { get; set; }
        public DateTime ReservationDate { get; set; }
        public string Description { get; set; }
        public int GuestNumber { get; set; }
        public ReservationStatus ReservationStatus { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
    }
}
