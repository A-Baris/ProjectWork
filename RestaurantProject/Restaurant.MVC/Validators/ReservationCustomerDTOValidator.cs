using FluentValidation;
using Restaurant.Entity.DTOs;

namespace Restaurant.MVC.Validators
{
    public class ReservationCustomerDToValidator :AbstractValidator<ReservationCustomerDTO>
    {
        public ReservationCustomerDToValidator()
        {
            RuleFor(x => x.Reservation.GuestNumber)
                .NotEmpty().WithMessage("Misafir Sayısı boş geçilemez")
                .GreaterThanOrEqualTo(1).WithMessage("Misafir Sayısı 1'den az olamaz");
            RuleFor(x => x.Reservation.ReservationDate)
            .Must(date => date >= DateTime.Today.AddDays(1)).WithMessage("Rezervasyon tarihi en az bir gün ileri olmalıdır");
             

        }
    }
}
