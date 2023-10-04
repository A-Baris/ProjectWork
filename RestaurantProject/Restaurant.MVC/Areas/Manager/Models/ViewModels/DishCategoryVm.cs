using System.ComponentModel.DataAnnotations;

namespace Restaurant.MVC.Areas.Manager.Models.ViewModels
{
    public class DishCategoryVm
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="boş bırakılamaz")]
        public string CategoryName { get; set; }
    }
}
