using FluentValidation;
using Restaurant.Entity.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.MVC.Validators
{
    public class TransactionVMValidator:AbstractValidator<TransactionVM>
    {
        public TransactionVMValidator()
        {
            RuleFor(x => x.Debit)
                .NotEmpty().WithMessage("Borç boş bırakılamaz")
                .GreaterThanOrEqualTo(1).WithMessage("Değer 0'da büyük olmalıdır");

            RuleFor(x => x.InvoiceCode)
                .NotEmpty().WithMessage("Fatura Kodu boş bırakılamaz")
                .MinimumLength(2).WithMessage("En az iki karakter olmalıdır");

            RuleFor(x => x.Payment)
              .GreaterThanOrEqualTo(0).WithMessage("Eksi değer girilemez");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Açıklama boş bırakılamaz")
                .MinimumLength(5).WithMessage("En az beş karakter olmaldır");

            RuleFor(x => x.SupplierId)
                .NotNull().NotEmpty().WithMessage("Lütfen seçim yapınız");




        }








        //[Display(Name = "Borç")]
        //public decimal Debit { get; set; }
        //[Display(Name = "Yapılan Ödeme")]
        //public decimal? Payment { get; set; }
        //[Display(Name = "Fatura Kodu")]
        //public string InvoiceCode { get; set; }
        //[Display(Name = "Son Ödeme Tarihi")]
        //public DateTime? LastPaymentDate { get; set; }
        //[Display(Name = "Açıklama")]
        //public string Description { get; set; }

        //public int SupplierId { get; set; }

    }
}
