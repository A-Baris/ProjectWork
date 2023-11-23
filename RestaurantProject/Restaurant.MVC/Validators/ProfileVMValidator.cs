using FluentValidation;
using Restaurant.MVC.Models.ViewModels;

namespace Restaurant.MVC.Validators
{
    public class ProfileVMValidator:AbstractValidator<ProfileVM>
    {
        public ProfileVMValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Kullanıcı Adı boş bırakılamaz")
                .MinimumLength(3).WithMessage("Kullanıcı Adı 3 karakterden az olamaz");

            RuleFor(x => x.CustomerName)
                .NotEmpty().WithMessage("Ad boş bırakılamaz");

            RuleFor(x => x.CustomerSurname)
             .NotEmpty().WithMessage("Soyad boş bırakılamaz");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email boş bırakılamaz")
                .EmailAddress().WithMessage("örn. abc@abc formatında olmalıdır");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Cep No boş bırakılamaz")
                .MinimumLength(10).MaximumLength(11).WithMessage("Cep No 5351112233 şeklinde veya başında 0 rakamıyla yazılmalıdır");

            


        }
    }
}
