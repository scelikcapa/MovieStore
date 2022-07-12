using FluentValidation;

namespace WebApi.Application.CustomerMoviesOperations.Commands.CreateCustomerMovies;

public class CreateCustomerMoviesCommandValidator : AbstractValidator<CreateCustomerMoviesCommand>
{
    public CreateCustomerMoviesCommandValidator()
    {
        RuleFor(q=>q.CustomerId).GreaterThan(0);
        RuleFor(q=>q.Model.MovieId).NotEmpty().GreaterThan(0);
    }
}