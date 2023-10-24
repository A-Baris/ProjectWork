using Restaurant.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Entity.ViewModels
{
    public class TransactionVM
    {
        public decimal Debit { get; set; }
        public decimal? Payment { get; set; }
        public string InvoiceCode { get; set; }    
        public DateTime? LastPaymentDate { get; set; }
        public string Description { get; set; }
        public int SupplierId { get; set; }

 
    }
}
