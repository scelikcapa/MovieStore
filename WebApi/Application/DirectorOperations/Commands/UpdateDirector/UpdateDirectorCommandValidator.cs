using FluentValidation;

namespace WebApi.Application.DirectorOperations.Commands.UpdateDirector;

public class UpdateDirectorCommandValidator : AbstractValidator<UpdateDirectorCommand>
{
    public UpdateDirectorCommandValidator()
    {
        RuleFor(cmd=>cmd.DirectorId).GreaterThan(0);
        RuleFor(cmd=>cmd.Model.Name).MinimumLength(3).When(cmd=> cmd.Model.Name is not null);
        RuleFor(cmd=>cmd.Model.Surname).MinimumLength(2).When(cmd=> cmd.Model.Surname is not null);
    }
}