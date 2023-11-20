using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Entity.Entities
{
    public class Customer:BaseEntity
    {
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string Surname { get; set; }
        [MaxLength(100)]
        public string? Adress { get; set; }
        [MaxLength(11)]
        public string? Phone { get; set; }
        [MaxLength(250)]
        public string Email { get; set; }

        public int? TableOfRestaurantId { get;set; }
        public TableOfRestaurant? TableOfRestaurant { get;set; }
        public virtual List<Reservation> Reservations { get; set; }


    }
}
