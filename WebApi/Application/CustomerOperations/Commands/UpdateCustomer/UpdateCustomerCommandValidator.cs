using FluentValidation;

namespace WebApi.Application.CustomerOperations.Commands.UpdateCustomer;

public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
{
    public UpdateCustomerCommandValidator()
    {
        RuleFor(cmd=>cmd.CustomerId).GreaterThan(0);
        RuleFor(cmd=>cmd.Model.Name).NotEmpty().MinimumLength(3);
        RuleFor(cmd=>cmd.Model.Surname).NotEmpty().MinimumLength(2);
    }
}