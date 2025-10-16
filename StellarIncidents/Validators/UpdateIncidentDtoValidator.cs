using FluentValidation;
using StellarIncidents.Application.Dtos;

namespace StellarIncidents.Validators
{
    public class UpdateIncidentDtoValidator : AbstractValidator<UpdateIncidentDto>
    {
        public UpdateIncidentDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("El título no puede estar vacío")
                .MaximumLength(200);

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("La descripción no puede estar vacía");

            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("El estado especificado no es válido");
        }
    }
}
