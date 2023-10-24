
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Entity.Entities
{
    public class Product : BaseEntity
    {
        [MaxLength(100)]
        public string ProductName { get; set; }
        public decimal Price { get; set; }
       
        [MaxLength(200)]
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public int? KitchenId { get; set; }
        public Kitchen Kitchen { get; set; }
        public int? MenuId { get; set; }
        public Menu Menu { get; set; }
        public int? SupplierId { get;set; }
        public Supplier Supplier { get; set; }
      

 

        public Category Category { get; set; }
        public IEnumerable<ProductIngredient> ProductIngredients { get; set; }
    }
}
