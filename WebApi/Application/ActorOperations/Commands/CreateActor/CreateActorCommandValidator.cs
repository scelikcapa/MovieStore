using FluentValidation;

namespace WebApi.Application.ActorOperations.Commands.CreateActor;

public class CreateActorCommandValidator : AbstractValidator<CreateActorCommand>
{
    public CreateActorCommandValidator()
    {
        RuleFor(cmd=>cmd.Model.Name).NotEmpty().MinimumLength(3);
        RuleFor(cmd=>cmd.Model.Surname).NotEmpty().MinimumLength(2);
    }
}