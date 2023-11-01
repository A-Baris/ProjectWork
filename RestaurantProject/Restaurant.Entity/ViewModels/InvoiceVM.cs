using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Entity.ViewModels
{
    public class InvoiceVM
    {
        
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
       
        public string? Description { get; set; }
        public decimal SubTotal { get; set; }
        public decimal KDV { get; set; }
        public decimal? Total { get; set; }

        public string CustomerName { get; set; }
      
        public string TaxAdress { get; set; }
       
        public string CompanyName { get; set; }
    }
}
