using FluentValidation;

namespace WebApi.Application.MovieOperations.Commands.CreateMovie;

public class CreateMovieCommandValidator : AbstractValidator<CreateMovieCommand>
{
    public CreateMovieCommandValidator()
    {
        RuleFor(p=>p.Model.DirectorId).NotEmpty().GreaterThan(0);
        RuleFor(p=>p.Model.GenreId).NotEmpty().GreaterThan(0);
        RuleFor(p=>p.Model.Name).NotEmpty().MinimumLength(3);
        RuleFor(p=>p.Model.Price).NotEmpty().GreaterThan(0);
        RuleFor(p=>p.Model.Year).NotEmpty().LessThanOrEqualTo(DateTime.Now.Year);
    }
}