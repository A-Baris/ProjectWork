using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Entity.DTOs
{
    public class ReservationCustomerUpdateDTO
    {
        public ReservationUpdateDTO ReservationUpdateDTO{get;set ;}
        public CustomerDTO CustomerDTO { get;set ;}
    }
}
