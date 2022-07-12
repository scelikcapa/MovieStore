using FluentValidation;

namespace WebApi.Application.ActorOperations.Commands.UpdateActor;

public class UpdateActorCommandValidator : AbstractValidator<UpdateActorCommand>
{
    public UpdateActorCommandValidator()
    {
        RuleFor(cmd=>cmd.ActorId).GreaterThan(0);
        RuleFor(cmd=>cmd.Model.Name).MinimumLength(3).When(cmd=> cmd.Model.Name is not null);
        RuleFor(cmd=>cmd.Model.Surname).MinimumLength(2).When(cmd=> cmd.Model.Surname is not null);
    }
}