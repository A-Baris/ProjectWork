using Restaurant.Entity.Enums;

namespace Restaurant.MVC.Areas.Manager.Models.ViewModels
{
    public class OrderCreateVM
    {
        public string? Description { get; set; }
        public OrderStatus Status { get; set; }
        public int TableofRestaurantId { get; set; }
        public int EmployeeId { get; set; }
        public int KitchenId { get; set; }
        public int ProductId { get; set; }
    }
}
