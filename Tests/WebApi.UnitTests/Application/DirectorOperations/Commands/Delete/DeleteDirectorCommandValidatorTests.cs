using FluentAssertions;
using WebApi.Application.DirectorOperations.Commands.DeleteDirector;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.DirectorOperations.Commands.Delete;

public class DeleteDirectorCommandValidatorTests : IClassFixture<CommonTestFixture>
{
    [Fact]
    public void WhenGivenDirectorIdIsNotGreaterThenZero_Validator_ShouldReturnError()
    {
        // Arrange
        DeleteDirectorCommand command = new DeleteDirectorCommand(null);
        command.DirectorId = 0;

        // Act
        DeleteDirectorCommandValidator validator = new DeleteDirectorCommandValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().BeGreaterThan(0);        
    }

    [Fact]
    public void WhenGivenDirectorIdIsGreaterThenZero_Validator_ShouldNotReturnError()
    {
        // Arrange
        DeleteDirectorCommand command = new DeleteDirectorCommand(null);
        command.DirectorId = 1;
        
        // Act
        DeleteDirectorCommandValidator validator = new DeleteDirectorCommandValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().Be(0);   
    }

}
