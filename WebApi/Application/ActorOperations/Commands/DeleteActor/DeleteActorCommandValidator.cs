using FluentValidation;

namespace WebApi.Application.ActorOperations.Commands.DeleteActor;

public class DeleteActorCommandValidator : AbstractValidator<DeleteActorCommand>
{
    public DeleteActorCommandValidator()
    {
        RuleFor(cmd => cmd.ActorId).GreaterThan(0);
    }
}