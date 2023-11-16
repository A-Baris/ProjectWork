using Restaurant.Entity.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Entity.ViewModels
{
    public class TransactionVM
    {
        [Display(Name = "Borç")]
        public decimal Debit { get; set; }
        [Display(Name = "Yapılan Ödeme")]
        public decimal? Payment { get; set; }
        [Display(Name = "Fatura Kodu")]
        public string InvoiceCode { get; set; }
        [Display(Name = "Son Ödeme Tarihi")]
        public DateTime? LastPaymentDate { get; set; }
        [Display(Name = "Açıklama")]
        public string Description { get; set; }

        public int SupplierId { get; set; }

 
    }
}
