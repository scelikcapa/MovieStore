using FluentValidation;

namespace WebApi.Application.CustomerOperations.Commands.CreateCustomer;

public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator()
    {
        RuleFor(cmd=>cmd.Model.Name).NotEmpty().MinimumLength(3);
        RuleFor(cmd=>cmd.Model.Surname).NotEmpty().MinimumLength(2);
    }
}