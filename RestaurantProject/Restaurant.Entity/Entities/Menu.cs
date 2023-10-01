using Restaurant.Entity.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Entity.Entities
{
    public class Menu : BaseEntity
    {
        [MaxLength(100)]
        public string MenuName { get; set; }
        public Decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal? Discount { get; set; }
        public IEnumerable<MenuDish> MenuDishes { get; set; }
        public IEnumerable<MenuDrink> MenuDrinks { get; set; }
        public IEnumerable<MenuOrder> MenuOrders { get; set; }
        public IEnumerable<MenuBill> MenuBills { get; set; }
    }

}