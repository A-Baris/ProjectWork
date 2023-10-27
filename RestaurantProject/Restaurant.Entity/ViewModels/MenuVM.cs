using System.ComponentModel.DataAnnotations;

namespace Restaurant.MVC.Areas.Manager.Models.ViewModels
{
    public class MenuVM
    {


        [Required(ErrorMessage ="boş bırakılamaz")]
        public string MenuName { get; set; }
    }
}
