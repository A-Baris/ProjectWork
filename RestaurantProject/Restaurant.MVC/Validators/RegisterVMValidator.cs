using FluentValidation;
using Restaurant.MVC.Models.ViewModels;

namespace Restaurant.MVC.Validators
{
    public class RegisterVMValidator:AbstractValidator<RegisterVM>
    {
        public RegisterVMValidator()
        {

            RuleFor(x => x.CustomerName)
                .NotEmpty().WithMessage("Müşteri Ad boş bırakılamaz");

            RuleFor(x => x.CustomerSurname)
                .NotEmpty().WithMessage("Müşteri Soyad boş bırakılamaz");


            RuleFor(x => x.UserName)
               .NotEmpty().WithMessage("Kullanıcı Adı boş geçilemez")
               .MinimumLength(3).WithMessage("Minimum 3 karakterden oluşmalı");

            RuleFor(x => x.Password)
             .NotEmpty().WithMessage("Şifre boş geçilemez");

            RuleFor(x => x.PasswordConfirmed)
             .NotEmpty().WithMessage("Şifre Tekrar boş geçilemez")
             .Equal(x => x.Password).WithMessage("Şifreler uyuşmuyor");

            RuleFor(x => x.Email)
        .NotEmpty().WithMessage("Email boş geçilemez")
        .EmailAddress().WithMessage("abc@xxx formatında giriş yapınız");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Cep No boş geçilemez")
                .MinimumLength(10).MaximumLength(11).WithMessage("Cep No 10 veya 11 karakter olarak girilmelidir\n örn(535xxx/0535xxx");
        }
    }
}
