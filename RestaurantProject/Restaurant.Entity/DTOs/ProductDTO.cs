using System.ComponentModel.DataAnnotations;

namespace Restaurant.API
{
    public class ProductDTO
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
