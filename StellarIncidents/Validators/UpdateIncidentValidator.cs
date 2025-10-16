using FluentValidation;
using StellarIncidents.Application.Dtos;

namespace StellarIncidents.Validators
{
    public class UpdateIncidentValidator : AbstractValidator<UpdateIncidentDto>
    {
        public UpdateIncidentValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("El título es obligatorio.")
                .MaximumLength(200);

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("La descripción no puede estar vacía.");

            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("El estado del incidente no es válido.");
        }
    }
}
