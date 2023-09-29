using Restaurant.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedDate{ get; set; }
        public DateTime UpdatedDate {get;set; }
        public BaseStatus BaseStatus { get; set; }
       


    }
}
