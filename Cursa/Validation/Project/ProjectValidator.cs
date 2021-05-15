using FluentValidation;

namespace Cursa.Validation.Project
{
    public class ProjectValidator: AbstractValidator<DataLayer.Entities.Project>
    {
        public ProjectValidator()
        {
            RuleFor(p => p.Code).NotEmpty();

        }
    }
}
