using FluentValidation;

namespace WebApi.Application.MovieOperations.Commands.CreateMovie;

public class CreateMovieCommandValidator : AbstractValidator<CreateMovieCommand>
{
    public CreateMovieCommandValidator()
    {
        RuleFor(cmd=>cmd.Model.DirectorId).NotEmpty().GreaterThan(0);
        RuleFor(cmd=>cmd.Model.GenreId).NotEmpty().GreaterThan(0);
        RuleFor(cmd=>cmd.Model.Name).NotEmpty().MinimumLength(3);
        RuleFor(cmd=>cmd.Model.Price).NotEmpty().GreaterThan(0);
        RuleFor(cmd=>cmd.Model.Year).NotEmpty().LessThanOrEqualTo(DateTime.Now.Year);
    }
}