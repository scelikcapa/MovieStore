using FluentValidation;
using WebApi.Application.CustomerOperations.Commands.AddCustomerGenre;

public class AddCustomerGenreCommandValidator : AbstractValidator<AddCustomerGenreCommand>
{
    public AddCustomerGenreCommandValidator()
    {
        RuleFor(cmd=> cmd.CustomerId).GreaterThan(0);
        RuleFor(cmd=> cmd.Model.GenreId).GreaterThan(0);
    }
}