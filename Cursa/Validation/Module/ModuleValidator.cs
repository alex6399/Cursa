using Cursa.ViewModels.ModuleVM;
using FluentValidation;

namespace Cursa.Validation.Module
{
    public class ModuleValidator : AbstractValidator<ModuleCreateEditViewModel>
    {
        public ModuleValidator()
        {
            RuleFor(x => x.ActualPlace)
                .NotNull()
                .When(x => x.ActualOrderCardId != null)
                .WithMessage("Поле обязательно для заполнения");
        }
    }
}