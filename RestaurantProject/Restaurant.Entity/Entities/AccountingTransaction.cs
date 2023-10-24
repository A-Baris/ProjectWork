using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Entity.Entities
{
    public class AccountingTransaction:BaseEntity
    {
        public decimal Debit { get; set; }
        public decimal? Payment { get; set; }      
        public string InvoiceCode { get; set; }
        public decimal? RemainingDebt
        {
            get
            {
                return Debit - Payment;
            }
            set
            {

            }
        }
        public DateTime? LastPaymentDate { get;set; }
        public string Description { get; set; }
        public int SupplierId { get; set; }

        public Supplier Supplier { get; set; }
    }
}
