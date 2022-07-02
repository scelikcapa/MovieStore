using FluentValidation;

namespace WebApi.Application.CustomerOperations.Queries.GetCustomerById;

public class GetCustomerGenresQueryValidator : AbstractValidator<GetCustomerGenresQuery>
{
    public GetCustomerGenresQueryValidator()
    {
        RuleFor(property=>property.CustomerId).GreaterThan(0);
    }
}