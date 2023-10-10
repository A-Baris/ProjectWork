using System.ComponentModel.DataAnnotations;

namespace Restaurant.MVC.Areas.Manager.Models.ViewModels
{
    public class RoleVM
    {
        public string Id { get; set; }
        [Required(ErrorMessage ="Boş geçilemez")]
        public string Name { get; set; }
    }
}
