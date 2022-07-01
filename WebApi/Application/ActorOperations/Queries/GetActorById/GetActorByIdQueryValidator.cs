using FluentValidation;

namespace WebApi.Application.ActorOperations.Queries.GetActorById;

public class GetActorByIdQueryValidator : AbstractValidator<GetActorByIdQuery>
{
    public GetActorByIdQueryValidator()
    {
        RuleFor(property=>property.ActorId).GreaterThan(0);
    }
}