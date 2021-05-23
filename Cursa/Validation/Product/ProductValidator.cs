using Cursa.ViewModels.ProductsVM;
using FluentValidation;

namespace Cursa.Validation.Product
{
    public class ProductValidator:AbstractValidator<ProductCreateViewModel>
    {
        public ProductValidator()
        {
            RuleFor(p => p.OrderDate)
                .NotNull()
                .WithMessage("Поле обязательно для заполнения");
            RuleFor(p => p.ManufacturingDate)
                .GreaterThan(x=>x.OrderDate)
                .When(x=>x.ManufacturingDate!=null)
                .WithMessage("Должна быть после заказа");
            RuleFor(p => p.ShippedDate)
                .GreaterThan(x=>x.ManufacturingDate)
                .When(x=>x.ShippedDate!=null)
                .WithMessage("Должна быть после изготовления");
        }
    }
}