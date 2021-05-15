using System;
using System.Linq;
using Cursa.ViewModels.EmployeesVM;
using FluentValidation;

namespace Cursa.Validation.Employee
{
    public class EmployeeCreateEditViewModelValidator : AbstractValidator<EmployeeCreateEditViewModel>
    {
        public EmployeeCreateEditViewModelValidator()
        {
            const string errorMessageNotNull = "Поле обязательно для заполнения";
            const string errorMessageMaxLength = "Максимальное количество символов 50";
            RuleFor(x => x.FirstName)
                .NotNull()
                .WithMessage(errorMessageNotNull)
                .NotEmpty()
                .WithMessage(errorMessageNotNull)
                .MaximumLength(50)
                .WithMessage(errorMessageMaxLength);
            RuleFor(x => x.MiddleName)
                .NotNull()
                .WithMessage(errorMessageNotNull)
                .NotEmpty()
                .WithMessage(errorMessageNotNull)
                .MaximumLength(50).WithMessage(errorMessageMaxLength);
            RuleFor(x => x.LastName)
                .NotNull()
                .WithMessage(errorMessageNotNull)
                .NotEmpty()
                .WithMessage(errorMessageNotNull)
                .MaximumLength(50).WithMessage(errorMessageMaxLength);
            RuleFor(x => x.Phone)
                .Matches("^((\\+7|7|8)+([0-9]){10})$")
                .WithMessage("Неверный формат");
        }
    }
}