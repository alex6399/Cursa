using Cursa.ViewModels.OrderCardVM;
using DataLayer;
using FluentValidation;

namespace Cursa.Validation.OrderCard
{
    public class OrderCardValidator: AbstractValidator<OrderCardCreateEditVM>
    {
        public OrderCardValidator()
        {
            RuleFor(x => x.ModulesVM).NotNull()
                .WithMessage("Не указаны модули УСО");   
        }
    }
}