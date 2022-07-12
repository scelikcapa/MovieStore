using FluentAssertions;
using WebApi.Application.DirectorOperations.Commands.CreateDirector;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.DirectorOperations.Commands.Create;

public class CreateDirectorCommandValidatorTests : IClassFixture<CommonTestFixture>
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
        CreateDirectorCommand command = new CreateDirectorCommand(null, null);
        command.Model = new CreateDirectorModel{
            Name = name, 
            Surname = surname};

        // Act
        CreateDirectorCommandValidator validator = new CreateDirectorCommandValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().BeInRange(1,2);      
    }

    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
    {
        // Arrange
        CreateDirectorCommand command = new CreateDirectorCommand(null, null);
        command.Model = new CreateDirectorModel{
            Name = "WhenValidInputsAreGiven_Validator_ShouldNotReturnError",
            Surname = "WhenValidInputsAreGiven_Validator_ShouldNotReturnError"};
            
        // Act
        CreateDirectorCommandValidator validator = new CreateDirectorCommandValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().Be(0);   
    }

}
