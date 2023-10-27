using Restaurant.Entity.Enums;

namespace Restaurant.MVC.Areas.Manager.Models.ViewModels
{
    public class OrderItemCreateVM
    {
        public OrderItemCreateVM()
        {
            Quantity = 1;
        }
        public int TableofRestaurantId { get; set; }
        public int Quantity { get; set; }
        public string? Description { get; set; }
        public int EmployeeId { get; set; }

        public int ProductId { get; set; }
    }
}
