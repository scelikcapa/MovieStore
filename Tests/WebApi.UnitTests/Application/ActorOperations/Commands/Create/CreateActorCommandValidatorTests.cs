using FluentAssertions;
using WebApi.Application.ActorOperations.Commands.CreateActor;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.ActorOperations.Commands.Create;

public class CreateActorCommandValidatorTests : IClassFixture<CommonTestFixture>
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
        CreateActorCommand command = new CreateActorCommand(null, null);
        command.Model = new CreateActorModel{
            Name = name, 
            Surname = surname};

        // Act
        CreateActorCommandValidator validator = new CreateActorCommandValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().BeInRange(1,2);      
    }

    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
    {
        // Arrange
        CreateActorCommand command = new CreateActorCommand(null, null);
        command.Model = new CreateActorModel{
            Name = "WhenValidInputsAreGiven_Validator_ShouldNotReturnError",
            Surname = "WhenValidInputsAreGiven_Validator_ShouldNotReturnError"};
            
        // Act
        CreateActorCommandValidator validator = new CreateActorCommandValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().Be(0);   
    }

}
