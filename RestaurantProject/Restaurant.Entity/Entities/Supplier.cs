using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Entity.Entities
{
    public class Supplier:BaseEntity
    {
        [MaxLength(100)]
        public string CompanyName { get; set; }
        [MaxLength(250)]
        public string Adress { get; set;}
        [MaxLength(15)]
        public string Phone { get; set; }
        [MaxLength(100)]
        public string Email { get; set; }
        [MaxLength(100)]
        public string ContactPerson { get; set; }
        [MaxLength(100)]
        public string Title { get; set; }
    
        [MaxLength(250)]
        public string? Description { get;set; }  

        public virtual List<AccountingTransaction> Transactions { get; set; }   
        public virtual List<Ingredient> Ingredients { get; set; }
        public virtual List<Product> Products { get; set; }
    }
}
