namespace Restaurant.MVC.Areas.Manager.Models.ViewModels
{
    public class DrinkVM
    {
        public int Id { get; set; }
        public string DrinkName { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public  int DrinkCategoryId { get; set; }
        public int KitchenId { get;set; }
        public int MenuId { get; set; }
    }
}
