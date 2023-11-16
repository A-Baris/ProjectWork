using System.ComponentModel.DataAnnotations;

namespace Restaurant.MVC.Areas.Manager.Models.ViewModels
{
    public class RecipeVM
    {
        public int ProductId { get; set; }
        public int IngredientId { get; set; }
        [Display(Name = "Miktar KG/Adet")]
        public decimal Quantity { get; set; }
    }
}
