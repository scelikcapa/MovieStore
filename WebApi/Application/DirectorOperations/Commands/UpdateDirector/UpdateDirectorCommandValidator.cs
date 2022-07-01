using FluentValidation;

namespace WebApi.Application.DirectorOperations.Commands.UpdateDirector;

public class UpdateDirectorCommandValidator : AbstractValidator<UpdateDirectorCommand>
{
    public UpdateDirectorCommandValidator()
    {
        RuleFor(cmd=>cmd.DirectorId).GreaterThan(0);
        RuleFor(cmd=>cmd.Model.Name).NotEmpty().MinimumLength(3);
        RuleFor(cmd=>cmd.Model.Surname).NotEmpty().MinimumLength(2);
    }
}