using System.ComponentModel.DataAnnotations;

namespace Restaurant.MVC.Areas.Manager.Models.ViewModels
{
    public class CategoryVm
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="boş bırakılamaz")]
        [Display(Name = "Kategori Adı")]
        public string CategoryName { get; set; }
    }
}
