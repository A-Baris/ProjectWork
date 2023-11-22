using FluentValidation;
using Restaurant.Entity.Entities;
using Restaurant.MVC.Areas.Manager.Models.ViewModels;

namespace Restaurant.MVC.Validators
{
    public class SuppliervMValidator : AbstractValidator<SupplierVM>
    {
        public SuppliervMValidator()
        {
            RuleFor(x => x.CompanyName)
                  .NotEmpty().WithMessage("Şirket Adı boş bırakılamaz");

            RuleFor(x => x.Adress)
                .NotEmpty().WithMessage("Adres boş bırakılamaz")
                .MinimumLength(5).WithMessage("Adres 5 karakterden az olamaz");

            RuleFor(x => x.Phone)
           .NotEmpty().WithMessage("Cep No boş bırakılamaz")
           .MinimumLength(10).WithMessage("Geçerli bir numara giriniz örn.(535) 111 11 11");

            RuleFor(x => x.Email)
           .NotEmpty().WithMessage("Email boş bırakılamaz")
           .EmailAddress().WithMessage("Email formatında giriş yapınız. 'abc@abc'");

            RuleFor(x => x.ContactPerson)
           .NotEmpty().WithMessage("Yetkili Kişi boş bırakılamaz");
            RuleFor(x => x.Title)
           .NotEmpty().WithMessage("Görevi boş bırakılamaz");



        }
    }
}
