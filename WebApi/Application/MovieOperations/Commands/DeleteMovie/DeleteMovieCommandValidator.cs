using FluentValidation;

namespace WebApi.Application.MovieOperations.Commands.DeleteMovie;

public class DeleteMovieCommandValidator : AbstractValidator<DeleteMovieCommand>
{
    public DeleteMovieCommandValidator()
    {
        RuleFor(cmd => cmd.MovieId).GreaterThan(0);
    }
}