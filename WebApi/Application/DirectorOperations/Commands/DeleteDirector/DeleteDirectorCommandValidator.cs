using FluentValidation;

namespace WebApi.Application.DirectorOperations.Commands.DeleteDirector;

public class DeleteDirectorCommandValidator : AbstractValidator<DeleteDirectorCommand>
{
    public DeleteDirectorCommandValidator()
    {
        RuleFor(cmd => cmd.DirectorId).GreaterThan(0);
    }
}