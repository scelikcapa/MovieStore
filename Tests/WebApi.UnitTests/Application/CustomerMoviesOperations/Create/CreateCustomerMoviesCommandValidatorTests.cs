using FluentAssertions;
using WebApi.Application.CustomerMoviesOperations.Commands.CreateCustomerMovies;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.CustomerMoviesOperations.Commands.Create;

public class CreateCustomerMoviesCommandValidatorTests : IClassFixture<CommonTestFixture>
{
    [Theory]
    [InlineData(0, 1)]
    [InlineData(1, 0)]
    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int customerId, int movieId)
    {
        // Arrange
        var command = new CreateCustomerMoviesCommand(null, null);
        command.CustomerId = customerId;
        command.Model = new CreateCustomerMoviesModel{MovieId = movieId};

        // Act
        CreateCustomerMoviesCommandValidator validator = new CreateCustomerMoviesCommandValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().BeGreaterThan(0);      
    }

    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
    {
        // Arrange
        var command = new CreateCustomerMoviesCommand(null, null);
        command.CustomerId = 1;
        command.Model = new CreateCustomerMoviesModel{MovieId = 1};
            
        // Act
        CreateCustomerMoviesCommandValidator validator = new CreateCustomerMoviesCommandValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().Be(0);   
    }

}
