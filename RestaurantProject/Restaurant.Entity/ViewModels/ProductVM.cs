using System.ComponentModel.DataAnnotations;

namespace Restaurant.MVC.Areas.Manager.Models.ViewModels
{
    public class ProductVM
    {

    
        public string ProductName { get; set; }
    
        public decimal Price { get; set; }

        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public int? SupplierId { get; set; }
        public int? KitchenId { get; set; }
        public int? MenuId { get; set; }
    }
}
