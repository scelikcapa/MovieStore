using FluentAssertions;
using WebApi.Application.CustomerOperations.Commands.UpdateCustomer;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.CustomerOperations.Commands.Update;

public class UpdateCustomerCommandValidatorTests : IClassFixture<CommonTestFixture>
{
    [Theory]
    [InlineData(0, null, null)]
    [InlineData(1, "", null)]
    [InlineData(1, "Na", null)]
    [InlineData(1, null, "")]
    [InlineData(1, null, "S")]
    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int customerId, string name, string surname)    
    {
        // Arrange
        UpdateCustomerCommand command = new UpdateCustomerCommand(null, null);
        command.CustomerId = customerId;
        command.Model = new UpdateCustomerModel{
            Name = name,
            Surname = surname};

        // Act
        UpdateCustomerCommandValidator validator = new UpdateCustomerCommandValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().Be(1);        
    }

    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
    {
        // Arrange
        UpdateCustomerCommand command = new UpdateCustomerCommand(null, null);
        command.CustomerId = 1;
        command.Model = new UpdateCustomerModel{
            Name = "WhenValidInputsAreGiven_Validator_ShouldNotReturnError",
            Surname = "WhenValidInputsAreGiven_Validator_ShouldNotReturnError"};

        // Act
        UpdateCustomerCommandValidator validator = new UpdateCustomerCommandValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().Be(0);   
    }

}
