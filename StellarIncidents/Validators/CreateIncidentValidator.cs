using FluentValidation;
using StellarIncidents.Application.Dtos;

namespace StellarIncidents.Validators
{
    public class CreateIncidentValidator : AbstractValidator<CreateIncidentDto>
    {
        public CreateIncidentValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("El título es obligatorio.")
                .MaximumLength(200).WithMessage("El título no puede superar los 200 caracteres.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("La descripción es obligatoria.");

            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("La categoría es obligatoria.");

            RuleFor(x => x.ReporterUserId)
                .NotEmpty().WithMessage("El usuario que reporta es obligatorio.");
        }
    }
}
