using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Entity.Entities
{
    public class Ingredient:BaseEntity
    {
       
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string Category { get; set; }
        public decimal Price { get;set; }
        public decimal Quantity { get; set; }
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }


        public IEnumerable<ProductIngredient> ProductIngredients { get; set; }
    }
}
