using FluentValidation;

namespace WebApi.Application.DirectorOperations.Commands.CreateDirector;

public class CreateDirectorCommandValidator : AbstractValidator<CreateDirectorCommand>
{
    public CreateDirectorCommandValidator()
    {
        RuleFor(cmd=>cmd.Model.Name).NotEmpty().MinimumLength(3);
        RuleFor(cmd=>cmd.Model.Surname).NotEmpty().MinimumLength(2);
    }
}