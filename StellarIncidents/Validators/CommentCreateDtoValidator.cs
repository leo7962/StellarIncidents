using FluentValidation;
using StellarIncidents.Application.Dtos;

namespace StellarIncidents.Validators;

public class CommentCreateDtoValidator : AbstractValidator<CommentCreateDto>
{
    public CommentCreateDtoValidator()
    {
        RuleFor(x => x.Text)
            .NotEmpty().WithMessage("El texto del comentario es obligatorio")
            .MaximumLength(500);

        RuleFor(x => x.AuthorUserId)
            .NotEmpty().WithMessage("Debe especificarse el autor del comentario");
    }
}