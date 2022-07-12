using FluentValidation;

namespace WebApi.Application.CustomerOperations.Commands.UpdateCustomer;

public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
{
    public UpdateCustomerCommandValidator()
    {
        RuleFor(cmd=>cmd.CustomerId).GreaterThan(0);
        RuleFor(cmd=>cmd.Model.Name).MinimumLength(3).When(cmd=> cmd.Model.Name is not null);
        RuleFor(cmd=>cmd.Model.Surname).MinimumLength(2).When(cmd=> cmd.Model.Surname is not null);
    }
}