using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Entity.ViewModels
{
    public class IngredientVM
    {
        [Required(ErrorMessage = "Boş geçilemez")]
        [Display(Name = "Malzeme Adı")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Boş geçilemez")]
  
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Boş geçilemez")]
        
        public int SupplierId { get; set; }
        [Required(ErrorMessage = "Boş geçilemez")]
        [Display(Name = "Birim Fiyat")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Boş geçilemez")]
        [Display(Name = "Miktar KG/Adet")]
        public decimal Quantity { get; set; }
    }
}
