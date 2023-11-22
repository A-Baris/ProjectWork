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
      
        [Display(Name = "Malzeme Adı")]
       
        public string Name { get; set; }
     

        public int CategoryId { get; set; }
     

        public int SupplierId { get; set; }
    
        [Display(Name = "Birim Fiyat")]
        public decimal Price { get; set; }
       
        [Display(Name = "Miktar KG/Adet")]
     
        public decimal Quantity { get; set; }
    }
}
