using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Entity.DTOs
{
    public class CustomerDTO
    {

      
        public string? Email { get; set; }
  
        public string? Phone { get; set; }
  
        public string? Adress { get; set; }

    }
}
