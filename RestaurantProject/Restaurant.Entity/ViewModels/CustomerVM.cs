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
        [Display(Name = "Müşteri Ad")]
        public string Name { get; set; }
        [Display(Name = "Müşteri Soyad")]
        public string Surname { get; set; }
        [Display(Name = "Adres")]
        public string? Adress { get; set; }
        [Display(Name = "Cep No")]
        public string Phone { get; set; }
        [Display(Name = "Email")]
        public string Email { get;set; }

        
     
    }
}
