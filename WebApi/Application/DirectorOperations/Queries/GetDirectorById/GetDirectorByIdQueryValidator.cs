using FluentValidation;

namespace WebApi.Application.DirectorOperations.Queries.GetDirectorById;

public class GetDirectorByIdQueryValidator : AbstractValidator<GetDirectorByIdQuery>
{
    public GetDirectorByIdQueryValidator()
    {
        RuleFor(property=>property.DirectorId).GreaterThan(0);
    }
}