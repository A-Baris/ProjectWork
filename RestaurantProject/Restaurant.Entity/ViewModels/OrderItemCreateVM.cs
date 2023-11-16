using Restaurant.Entity.Enums;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.MVC.Areas.Manager.Models.ViewModels
{
    public class OrderItemCreateVM
    {
        public OrderItemCreateVM()
        {
            Quantity = 1;
        }
       
        public int TableofRestaurantId { get; set; }
        [Display(Name = "Sipariş Adet")]
        public int Quantity { get; set; }
        [Display(Name = "Açıklama")]
        public string? Description { get; set; }
        public int EmployeeId { get; set; }

        public int ProductId { get; set; }
    }
}
