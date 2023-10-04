using System.ComponentModel.DataAnnotations;

namespace Restaurant.MVC.Areas.Manager.Models.ViewModels
{
    public class KitchenVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Boş geçilemez")]
        public string KitchenName { get; set; }
        public string Description { get; set; }
    }
}
