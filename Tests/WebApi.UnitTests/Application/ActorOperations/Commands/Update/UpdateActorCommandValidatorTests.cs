using FluentAssertions;
using WebApi.Application.ActorOperations.Commands.UpdateActor;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.ActorOperations.Commands.Update;

public class UpdateActorCommandValidatorTests : IClassFixture<CommonTestFixture>
{
    [Theory]
    [InlineData(0, null, null)]
    [InlineData(1, "", null)]
    [InlineData(1, "Na", null)]
    [InlineData(1, null, "")]
    [InlineData(1, null, "S")]
    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int actorId, string name, string surname)    
    {
        // Arrange
        UpdateActorCommand command = new UpdateActorCommand(null, null);
        command.ActorId = actorId;
        command.Model = new UpdateActorModel{
            Name = name,
            Surname = surname};

        // Act
        UpdateActorCommandValidator validator = new UpdateActorCommandValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().Be(1);        
    }

    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
    {
        // Arrange
        UpdateActorCommand command = new UpdateActorCommand(null, null);
        command.ActorId = 1;
        command.Model = new UpdateActorModel{
            Name = "WhenValidInputsAreGiven_Validator_ShouldNotReturnError",
            Surname = "WhenValidInputsAreGiven_Validator_ShouldNotReturnError"};

        // Act
        UpdateActorCommandValidator validator = new UpdateActorCommandValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().Be(0);   
    }

}
