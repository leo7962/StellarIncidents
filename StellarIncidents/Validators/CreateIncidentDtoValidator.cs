using FluentValidation;
using StellarIncidents.Application.Dtos;

namespace StellarIncidents.Validators;

public class CreateIncidentDtoValidator : AbstractValidator<CreateIncidentDto>
{
    public CreateIncidentDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("El título es obligatorio")
            .MaximumLength(200);

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("La descripción es obligatoria");

        RuleFor(x => x.CategoryId)
            .NotEmpty().WithMessage("Debe seleccionar una categoría válida");

        RuleFor(x => x.ReporterUserId)
            .NotEmpty().WithMessage("Debe indicar el usuario que reporta el incidente");
    }
}