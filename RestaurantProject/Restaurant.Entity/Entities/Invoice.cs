using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Entity.Entities
{
    public class Invoice:BaseEntity
    {
        [MaxLength(100)]
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        [MaxLength(250)]
        public string? Description { get; set; }
        public decimal SubTotal { get; set; }
        public decimal KDV { get; set; }
        public decimal? Total {
            get
            {
                return SubTotal * KDV;
            }
            set
            {

            }
        
        
        }
        [MaxLength(200)]
        public string CustomerName { get; set; }
        [MaxLength(200)]
        public string TaxAdress { get; set; }
        [MaxLength (200)]
        public string CompanyName { get; set; }


    }
}
