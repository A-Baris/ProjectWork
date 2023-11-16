using System.ComponentModel.DataAnnotations;

namespace Restaurant.Entity.ViewModels
{
    public class KitchenVM
    {
      
        [Display(Name = "Mutfak Adı")]
        public string KitchenName { get; set; }
        [Display(Name = "Açıklama")]
        public string Description { get; set; }
    }
}
