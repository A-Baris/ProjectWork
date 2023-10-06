using System.ComponentModel.DataAnnotations;

namespace Restaurant.MVC.Areas.Manager.Models.ViewModels
{
    public class MenuVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="boş bırakılamaz")]
        public string MenuName { get; set; }
    }
}
