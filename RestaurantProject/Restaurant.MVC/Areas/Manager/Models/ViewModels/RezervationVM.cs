namespace Restaurant.MVC.Areas.Manager.Models.ViewModels
{
    public class RezervationVM
    {
        public int TableOfRestaurantId { get; set; }
        public string TableName { get;set; }
        public string TableLocation { get;set; }
        public string ReserveStatus { get; set; }
        public string CustomerName { get; set; }
        public DateTime ReservationDate { get; set; }
    }
}
