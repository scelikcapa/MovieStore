using FluentAssertions;
using WebApi.Application.CustomerOperations.Commands.DeleteCustomer;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.CustomerOperations.Commands.Delete;

public class DeleteCustomerCommandValidatorTests : IClassFixture<CommonTestFixture>
{
    [Fact]
    public void WhenGivenCustomerIdIsNotGreaterThenZero_Validator_ShouldReturnError()
    {
        // Arrange
        DeleteCustomerCommand command = new DeleteCustomerCommand(null);
        command.CustomerId = 0;

        // Act
        DeleteCustomerCommandValidator validator = new DeleteCustomerCommandValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().BeGreaterThan(0);        
    }

    [Fact]
    public void WhenGivenCustomerIdIsGreaterThenZero_Validator_ShouldNotReturnError()
    {
        // Arrange
        DeleteCustomerCommand command = new DeleteCustomerCommand(null);
        command.CustomerId = 1;
        
        // Act
        DeleteCustomerCommandValidator validator = new DeleteCustomerCommandValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().Be(0);   
    }

}
