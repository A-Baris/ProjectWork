using System.ComponentModel.DataAnnotations;

namespace Restaurant.MVC.Areas.Manager.Models.ViewModels
{
    public class ProductVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="boş bırakılamaz")]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "boş geçilemez")]
        [Range(0, int.MaxValue, ErrorMessage = "The property must be a non-negative integer.")]
        public decimal Price { get; set; }

        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public int? KitchenId { get; set; }
        public int? MenuId { get; set; }
    }
}
