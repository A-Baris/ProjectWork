using System.ComponentModel.DataAnnotations;

namespace Restaurant.MVC.Areas.Manager.Models.ViewModels
{
    public class IngredientUpdateVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Boş geçilemez")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Boş geçilemez")]
        public string Category { get; set; }
        [Required(ErrorMessage = "Boş geçilemez")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Boş geçilemez")]
        public decimal Quantity { get; set; }

    }
}
