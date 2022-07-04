using FluentValidation;

namespace WebApi.Application.CustomerMoviesOperations.Queries.GetCustomerMoviesById;

public class GetCustomerMoviesByIdQueryValidator : AbstractValidator<GetCustomerMoviesByIdQuery>
{
    public GetCustomerMoviesByIdQueryValidator()
    {
        RuleFor(q=>q.CustomerId).GreaterThan(0);
    }
}