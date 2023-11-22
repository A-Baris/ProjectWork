using System.ComponentModel.DataAnnotations;

namespace Restaurant.Entity.ViewModels
{
    public class CategoryVm
    {
       
      
        [Display(Name = "Kategori Adı")]
        public string CategoryName { get; set; }
    }
}
