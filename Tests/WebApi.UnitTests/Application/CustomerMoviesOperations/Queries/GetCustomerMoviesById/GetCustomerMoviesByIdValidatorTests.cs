using FluentAssertions;
using WebApi.Application.CustomerMoviesOperations.Queries.GetCustomerMoviesById;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.CustomerMoviesOperations.Queries.GetCustomerMoviesById;

public class GetCustomerMoviesByIdQueryValidatorTests : IClassFixture<CommonTestFixture>
{
    [Fact]
    public void WhenGivenCustomerIdIsNotGreaterThenZero_Validator_ShouldBeReturnError()
    {
        // Arrange
        GetCustomerMoviesByIdQuery command = new GetCustomerMoviesByIdQuery(null, null);
        command.CustomerId = 0;

        // Act
        GetCustomerMoviesByIdQueryValidator validator = new GetCustomerMoviesByIdQueryValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().BeGreaterThan(0);        
    }

    [Fact]
    public void WhenGivenCustomerIdIsGreaterThenZero__Validator_ShouldNotReturnError()
    {
        // Arrange
        GetCustomerMoviesByIdQuery command = new GetCustomerMoviesByIdQuery(null, null);
        command.CustomerId = 1;
        
        // Act
        GetCustomerMoviesByIdQueryValidator validator = new GetCustomerMoviesByIdQueryValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().Be(0);   
    }

}
