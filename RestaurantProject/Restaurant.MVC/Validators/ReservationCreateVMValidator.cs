using FluentValidation;
using Restaurant.Entity.ViewModels;

namespace Restaurant.MVC.Validators
{
    public class ReservationCreateVMValidator : AbstractValidator<ReservationCreateVM>
    {
        public ReservationCreateVMValidator()
        {
            RuleFor(x => x.ReservationDate)
                .NotEmpty().WithMessage("Rezervasyon Tarih boş bırakılamaz")
                 .Must(x => x.Date.DayOfYear >= DateTime.Now.DayOfYear).WithMessage("Rezervasyon Tarihi geçmiş tarih için seçilemez");



            RuleFor(x => x.GuestNumber)
                .NotEmpty().WithMessage("Misafir Sayısı boş bırakılamaz")
                .GreaterThanOrEqualTo(1).WithMessage("Misafir Sayısı 1'den az olamaz");


            //Eğer Customer seçilmemişse yeni bir müşteri ekleme ihtiyacı doğacağından
            //Customer property ler girilmelidir.
            RuleFor(x => x.CustomerVM)  
            .SetValidator(new CustomerVMValidator())
            .When(x => x.CustomerId == null);
        }
    }
}
