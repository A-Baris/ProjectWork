using FluentValidation;
using Restaurant.MVC.Models.ViewModels;

namespace Restaurant.MVC.Validators
{
    public class ProfileVMValidators:AbstractValidator<ProfileVM>
    {
        public ProfileVMValidators()
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .NotNull()
                .WithMessage("Kullanıcı Adı boş bırakılamaz");
            RuleFor(x => x.CustomerName)
                .NotEmpty()
                .NotNull()
                .WithMessage("Ad boş bırakılamaz");
            RuleFor(x => x.Email)
                .NotEmpty()
                .NotNull()
                .WithMessage("Email boş bırakılamaz");
            RuleFor(x => x.CustomerSurname)
                .NotEmpty()
                .NotNull()
                .WithMessage("Soyad boş bırakılamaz");
            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .NotNull()
                .WithMessage("Cep No boş bırakılamaz");
            


        }
    }
}
