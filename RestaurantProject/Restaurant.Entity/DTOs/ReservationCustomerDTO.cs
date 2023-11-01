using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Entity.DTOs
{
    public class ReservationCustomerDTO
    {
        public ReservationDTO Reservation { get; set; }
        public CustomerDTO Customer { get; set; }
    }
}
