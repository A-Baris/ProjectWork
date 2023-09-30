using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entities
{
    public class Customer:BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Adress { get; set; }
        public string Phone { get; set; }

        public int TableOfRestaurantId { get;set; }
        public TableOfRestaurant TableOfRestaurant { get;set; }
        public IEnumerable<BillCustomer> BillCustomers { get; set;}

    }
}
