using Cursa.ViewModels.Users;
using FluentValidation;

namespace Cursa.Validation.Users
{
    public class NecessarilyChangePasswordViewModelValidator : AbstractValidator<NecessarilyChangePasswordViewModel>
    {
        public NecessarilyChangePasswordViewModelValidator()
        {
            RuleFor(p => p.OldPassword).NotEqual(p => p.NewPassword)
                .WithMessage("Новый пароль должен отличаться от старого!");
        }
    }
}