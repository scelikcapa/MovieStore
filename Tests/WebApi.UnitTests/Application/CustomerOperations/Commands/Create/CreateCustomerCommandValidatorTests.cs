using FluentAssertions;
using WebApi.Application.CustomerOperations.Commands.CreateCustomer;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.CustomerOperations.Commands.Create;

public class CreateCustomerCommandValidatorTests : IClassFixture<CommonTestFixture>
{
    [Theory]
    [InlineData(null, "Surname")]
    [InlineData("", "Surname")]
    [InlineData("Na", "Surname")]
    [InlineData("Nam", null)]
    [InlineData("Nam", "")]
    [InlineData("Nam", "S")]
    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string name, string surname)
    {
        // Arrange
        CreateCustomerCommand command = new CreateCustomerCommand(null, null);
        command.Model = new CreateCustomerModel{
            Name = name, 
            Surname = surname};

        // Act
        CreateCustomerCommandValidator validator = new CreateCustomerCommandValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().BeInRange(1,2);      
    }

    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
    {
        // Arrange
        CreateCustomerCommand command = new CreateCustomerCommand(null, null);
        command.Model = new CreateCustomerModel{
            Name = "WhenValidInputsAreGiven_Validator_ShouldNotReturnError",
            Surname = "WhenValidInputsAreGiven_Validator_ShouldNotReturnError"};
            
        // Act
        CreateCustomerCommandValidator validator = new CreateCustomerCommandValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().Be(0);   
    }

}
