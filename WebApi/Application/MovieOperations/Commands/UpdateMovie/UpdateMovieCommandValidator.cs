using FluentValidation;

namespace WebApi.Application.MovieOperations.Commands.UpdateMovie;

public class UpdateMovieCommandValidator : AbstractValidator<UpdateMovieCommand>
{
    public UpdateMovieCommandValidator()
    {
        RuleFor(cmd=>cmd.MovieId).GreaterThan(0);
        RuleFor(cmd=>cmd.Model.Title).MinimumLength(3).When(cmd=>cmd.Model.Title is not null);
        RuleFor(cmd=>cmd.Model.Year).LessThanOrEqualTo(DateTime.Now.Year).When(cmd=>cmd.Model.Year is not null);
        RuleFor(cmd=>cmd.Model.Price).GreaterThan(0).When(cmd=>cmd.Model.Price is not null);
        RuleFor(cmd=>cmd.Model.GenreId).GreaterThan(0).When(cmd=>cmd.Model.GenreId is not null);
        RuleFor(cmd=>cmd.Model.DirectorId).GreaterThan(0).When(cmd=>cmd.Model.DirectorId is not null);
    }
}