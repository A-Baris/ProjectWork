using System.ComponentModel.DataAnnotations;

namespace Restaurant.MVC.Areas.Manager.Models.ViewModels
{
    public class IngredientCategoryVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="boş bırakılamaz")]
        public string CategoryName { get; set; }
            
        [Required(ErrorMessage = "boş bırakılamaz")]
        public string Description { get; set; }
    }
}
