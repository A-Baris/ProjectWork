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
       
        [Display(Name = "Fatura Tarih")]
        public DateTime InvoiceDate { get; set; }
        [Display(Name = "Açıklama")]
        public string? Description { get; set; }
        [Display(Name = "Ara Toplam")]
        public decimal SubTotal { get; set; }
        [Display(Name = "KDV")]
        public decimal KDV { get; set; }
        [Display(Name = "Toplam Tutar")]
        public decimal? Total { get; set; }
        [Display(Name = "Müşteri Adı")]
        public string CustomerName { get; set; }
        [Display(Name = "Vergi Dairesi Adres")]

        public string TaxAdress { get; set; }
        [Display(Name = "Mükellef Şirketimiz")]
        public string CompanyName { get; set; }
    }
}
