using Restaurant.Entity.Enums;
using Restaurant.Entity.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Entity.Entities
{
    public class Order:BaseEntity
    {
        
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }

        [MaxLength(200)]
        public string? Description { get; set; }
        public OrderStatus StatusOfOrder { get; set; }
        public int TableofRestaurantId { get;set; }
        public int EmployeeId { get;set; }
 
       public Product Product { get; set; }
        public Employee Employee { get;set; }
        public TableOfRestaurant TableOfRestaurant { get; set; }

 


    }
}
