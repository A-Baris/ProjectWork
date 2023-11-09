using System.ComponentModel.DataAnnotations;

namespace Restaurant.Entity.ViewModels
{
    public class ProductVM
    {

        
     
        public string ProductName { get; set; }

        public decimal Price { get; set; }

        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public int CategoryId { get; set; }
      
        public int? KitchenId { get; set; }
        public int? MenuId { get; set; }
        public int? SupplierId { get; set; }
    }
}
