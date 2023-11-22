using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Entity.ViewModels
{
    public class EmployeeVM
    {
        [Display(Name = "Çalışan Ad")]
        public string Name { get; set; }

        [Display(Name = "Çalışan Soyad")]
        public string Surname { get; set; }


        [Display(Name = "Cep No")]
        public string Phone { get; set; }

        [Display(Name = "T.C. Kimlik")]
        public string? TcNo { get; set; }

        [Display(Name = "Görevi")]
        public string Title { get; set; }
        [Display(Name = "Notlar")]
        public string? Notes { get; set; }
    }
}
