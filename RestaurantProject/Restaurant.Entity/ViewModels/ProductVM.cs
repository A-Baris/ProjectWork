using System.ComponentModel.DataAnnotations;

namespace Restaurant.Entity.ViewModels
{
    public class ProductVM
    {


        [Display(Name = "Ürün Adı")]
        public string ProductName { get; set; }
        [Display(Name = "Fiyat")]
        public decimal Price { get; set; }
        [Display(Name = "Açıklama")]
        public string? Description { get; set; }
        [Display(Name = "Image")]
        public string? ImageUrl { get; set; }
        public int CategoryId { get; set; }
      
        public int? KitchenId { get; set; }
        public int? MenuId { get; set; }
        public int? SupplierId { get; set; }
    }
}
