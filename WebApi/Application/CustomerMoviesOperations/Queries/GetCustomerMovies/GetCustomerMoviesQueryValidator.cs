using FluentValidation;

namespace WebApi.Application.CustomerMoviesOperations.Queries.GetCustomerMovies;

public class GetCustomerMoviesQueryValidator : AbstractValidator<GetCustomerMoviesQuery>
{
    public GetCustomerMoviesQueryValidator()
    {
        RuleFor(q=>q.CustomerId).GreaterThan(0);
    }
}