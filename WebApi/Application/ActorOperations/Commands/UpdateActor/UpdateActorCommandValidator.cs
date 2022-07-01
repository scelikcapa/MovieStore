using FluentValidation;

namespace WebApi.Application.ActorOperations.Commands.UpdateActor;

public class UpdateActorCommandValidator : AbstractValidator<UpdateActorCommand>
{
    public UpdateActorCommandValidator()
    {
        RuleFor(cmd=>cmd.ActorId).GreaterThan(0);
        RuleFor(cmd=>cmd.Model.Name).NotEmpty().MinimumLength(3);
        RuleFor(cmd=>cmd.Model.Surname).NotEmpty().MinimumLength(2);
    }
}