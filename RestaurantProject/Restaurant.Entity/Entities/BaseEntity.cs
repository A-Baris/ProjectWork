using Restaurant.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Entity.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
       
        public string CreatedComputerName { get; set; }
        public string CreatedIpAddress { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedComputerName { get; set; }
        public string? UpdatedIpAddress { get; set; }
        public BaseStatus BaseStatus { get; set; }





    }
}
