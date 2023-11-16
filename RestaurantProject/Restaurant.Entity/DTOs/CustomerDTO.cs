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

        [Display(Name = "Email")]
        public string? Email { get; set; }
        [Display(Name = "Cep No")]
        public string? Phone { get; set; }
        [Display(Name = "Adres")]
        public string? Adress { get; set; }

    }
}
