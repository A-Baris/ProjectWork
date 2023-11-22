using FluentValidation;
using Restaurant.Entity.Enums;
using Restaurant.Entity.ViewModels;

namespace Restaurant.MVC.Validators
{
    public class TableOfRestaurantVMValidator:AbstractValidator<TableOfRestaurantVM>
    {
        public TableOfRestaurantVMValidator()
        {
            RuleFor(x => x.TableName)
                .NotEmpty().WithMessage("Masa Adı boş geçilemez");

            RuleFor(x => x.TableLocation)
             .NotEmpty().WithMessage("Lütfen bir seçim yapınız");

            RuleFor(x => x.TableCapacity)
             .NotEmpty().WithMessage("Masa Kapasitesi boş geçilemez")
             .GreaterThanOrEqualTo(2).WithMessage("Masa Kapasite 2 'den az olamaz");


            RuleFor(x => x.EmployeeId)
             .NotEmpty().WithMessage("Lütfen bir seçim yapınız");




           
        }
    }
}
