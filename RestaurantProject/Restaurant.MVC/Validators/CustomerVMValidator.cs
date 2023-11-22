using FluentValidation;
using Restaurant.Entity.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.MVC.Validators
{
    public class CustomerVMValidator:AbstractValidator<CustomerVM>
    {
        public CustomerVMValidator()
        {

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Müşteri Adı boş bırakılamaz");
              

            RuleFor(x => x.Surname)
             .NotEmpty().WithMessage("Müşteri Soyadı boş bırakılamaz");

            RuleFor(x => x.Adress)
             .NotEmpty().WithMessage("Adres boş bırakılamaz")
             .MinimumLength(3).WithMessage("En az üç karakter olmalıdır");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Cep No boş bırakılamaz");



            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email Adresi boş bırakılamaz")
                .EmailAddress().WithMessage("Email 'abc@xxx.xxx' formatında olmalıdır");
              


        }






      
    }
}
