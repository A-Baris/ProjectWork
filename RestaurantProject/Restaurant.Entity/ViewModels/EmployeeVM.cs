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
        //public int Id { get; set; }
        [Required(ErrorMessage = "Boş bırakılamaz")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Boş bırakılamaz")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Boş bırakılamaz")]
        [Phone]
        public string Phone { get; set; }


        public string? TcNo { get; set; }
        [Required(ErrorMessage = "Boş bırakılamaz")]
        public string Title { get; set; }

        public string? Notes { get; set; }
    }
}
