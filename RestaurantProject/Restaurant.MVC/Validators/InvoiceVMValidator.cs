using FluentValidation;
using Restaurant.Entity.ViewModels;

namespace Restaurant.MVC.Validators
{
    public class InvoiceVMValidator:AbstractValidator<InvoiceVM>
    {
        public InvoiceVMValidator()
        {
            RuleFor(x => x.InvoiceDate)
                .NotEmpty().WithMessage("Fatura Tarihi boş bırakılamaz");
            RuleFor(x => x.SubTotal)
               .NotEmpty().WithMessage("Ara Toplam boş bırakılamaz")
               .GreaterThan(1).WithMessage("Ara Toplam 1'den küçük olamaz");

            RuleFor(x => x.KDV)
               .NotEmpty().WithMessage("KDV boş bırakılamaz")
               .GreaterThan(1).WithMessage("KDV çarpanı 1 küsürattan az olamaz");

            RuleFor(x => x.CustomerName)
               .NotEmpty().WithMessage("Müşteri Adı boş bırakılamaz");
            RuleFor(x => x.TaxAdress)
               .NotEmpty().WithMessage("Vergi Dairesi Adresi boş bırakılamaz");
            RuleFor(x => x.CompanyName)
               .NotEmpty().WithMessage("Mükellef Şirketimiz boş bırakılamaz");


           
        }
    }
}
