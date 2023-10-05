using System.ComponentModel.DataAnnotations;

namespace Restaurant.MVC.Areas.Manager.Models.ViewModels
{
    public class DrinkCategoryVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Boş bırakılamaz")]
        public string CategoryName { get; set; }
        public string? Description { get; set; }
    }
}
