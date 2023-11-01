using Restaurant.Entity.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Entity.ViewModels
{
    public class CustomerVM
    {
       
        public string Name { get; set; }
      
        public string Surname { get; set; }
       
        public string? Adress { get; set; }
      
        public string Phone { get; set; }
        public string Email { get;set; }

        
     
    }
}
