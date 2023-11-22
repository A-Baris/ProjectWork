using FluentValidation;
using Restaurant.MVC.Areas.Manager.Models.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.MVC.Validators
{
    public class OrderItemCreateVMValidator:AbstractValidator<OrderItemCreateVM>
    {
        public OrderItemCreateVMValidator()
        {
            RuleFor(x => x.Quantity)
                .NotEmpty().WithMessage("Sipariş Adet boş bırakılamaz")
                .GreaterThanOrEqualTo(1).WithMessage("Siarpiş Adet 1'den az olamaz");
                



    }
    }
}
