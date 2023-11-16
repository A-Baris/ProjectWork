using System.ComponentModel.DataAnnotations;

namespace Restaurant.MVC.Areas.Manager.Models.ViewModels
{
    public class MenuVM
    {


        [Required(ErrorMessage ="boş bırakılamaz")]
        [Display(Name = "Menu Adı")]
        public string MenuName { get; set; }
    }
}
