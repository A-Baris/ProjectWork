using FluentValidation;
using Restaurant.Entity.ViewModels;

namespace Restaurant.MVC.Validators
{
    public class EmployeeVMValidator : AbstractValidator<EmployeeVM>

    {
        public EmployeeVMValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Çalışan Ad boş bırakılamaz");

            RuleFor(x => x.Surname)
                .NotEmpty().WithMessage("Çalışan Soyad boş bırakılamaz");

            RuleFor(x => x.Phone)
           .NotEmpty().WithMessage("Cep No  boş bırakılamaz");

            RuleFor(x => x.TcNo)
           .NotEmpty().WithMessage("T.C. No boş bırakılamaz")
           .MinimumLength(11).WithMessage("T.C. No 11 karakterden oluşmalıdır");

            RuleFor(x => x.Title)
           .NotEmpty().WithMessage("Görev boş bırakılamaz");


           
        }

    }

}
