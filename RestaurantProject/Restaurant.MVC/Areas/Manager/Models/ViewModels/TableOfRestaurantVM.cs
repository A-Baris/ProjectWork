namespace Restaurant.MVC.Areas.Manager.Models.ViewModels
{
    public class TableOfRestaurantVM
    {
        public string Location { get; set; }
        public string TableName { get; set; }
        public int Capacity { get; set; }
        public int WaiterId { get; set; }
    }
}
