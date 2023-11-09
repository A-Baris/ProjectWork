using System.ComponentModel.DataAnnotations;

namespace Restaurant.Entity.ViewModels
{
    public class KitchenVM
    {
        public int Id { get; set; }
    
        public string KitchenName { get; set; }
        public string Description { get; set; }
    }
}
