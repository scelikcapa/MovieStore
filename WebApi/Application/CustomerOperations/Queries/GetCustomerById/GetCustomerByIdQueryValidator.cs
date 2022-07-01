using FluentValidation;

namespace WebApi.Application.CustomerOperations.Queries.GetCustomerById;

public class GetCustomerByIdQueryValidator : AbstractValidator<GetCustomerByIdQuery>
{
    public GetCustomerByIdQueryValidator()
    {
        RuleFor(property=>property.CustomerId).GreaterThan(0);
    }
}