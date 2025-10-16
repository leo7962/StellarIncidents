using FluentValidation;
using StellarIncidents.Application.Dtos;

namespace StellarIncidents.Validators;

public class CommentCreateValidator : AbstractValidator<CommentCreateDto>
{
    public CommentCreateValidator()
    {
        RuleFor(x => x.Text)
            .NotEmpty().WithMessage("El comentario no puede estar vacío.")
            .MaximumLength(500).WithMessage("El comentario no puede tener más de 500 caracteres.");

        RuleFor(x => x.AuthorUserId)
            .NotEmpty().WithMessage("El autor del comentario es obligatorio.");
    }
}