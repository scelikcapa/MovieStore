using FluentValidation;

namespace WebApi.Application.MovieOperations.Queries.GetMovieById;

public class GetMovieByIdQueryValidator : AbstractValidator<GetMovieByIdQuery>
{
    public GetMovieByIdQueryValidator()
    {
        RuleFor(property=>property.MovieId).GreaterThan(0);
    }
}