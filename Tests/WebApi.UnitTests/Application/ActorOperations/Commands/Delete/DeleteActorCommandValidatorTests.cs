using FluentAssertions;
using WebApi.Application.ActorOperations.Commands.DeleteActor;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.ActorOperations.Commands.Delete;

public class DeleteActorCommandValidatorTests : IClassFixture<CommonTestFixture>
{
    [Fact]
    public void WhenGivenActorIdIsNotGreaterThenZero_Validator_ShouldReturnError()
    {
        // Arrange
        DeleteActorCommand command = new DeleteActorCommand(null);
        command.ActorId = 0;

        // Act
        DeleteActorCommandValidator validator = new DeleteActorCommandValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().BeGreaterThan(0);        
    }

    [Fact]
    public void WhenGivenActorIdIsGreaterThenZero_Validator_ShouldNotReturnError()
    {
        // Arrange
        DeleteActorCommand command = new DeleteActorCommand(null);
        command.ActorId = 1;
        
        // Act
        DeleteActorCommandValidator validator = new DeleteActorCommandValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().Be(0);   
    }

}
