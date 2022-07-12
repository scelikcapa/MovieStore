using FluentAssertions;
using WebApi.Application.DirectorOperations.Commands.UpdateDirector;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.DirectorOperations.Commands.Update;

public class UpdateDirectorCommandValidatorTests : IClassFixture<CommonTestFixture>
{
    [Theory]
    [InlineData(0, null, null)]
    [InlineData(1, "", null)]
    [InlineData(1, "Na", null)]
    [InlineData(1, null, "")]
    [InlineData(1, null, "S")]
    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int directorId, string name, string surname)    
    {
        // Arrange
        UpdateDirectorCommand command = new UpdateDirectorCommand(null, null);
        command.DirectorId = directorId;
        command.Model = new UpdateDirectorModel{
            Name = name,
            Surname = surname};

        // Act
        UpdateDirectorCommandValidator validator = new UpdateDirectorCommandValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().Be(1);        
    }

    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
    {
        // Arrange
        UpdateDirectorCommand command = new UpdateDirectorCommand(null, null);
        command.DirectorId = 1;
        command.Model = new UpdateDirectorModel{
            Name = "WhenValidInputsAreGiven_Validator_ShouldNotReturnError",
            Surname = "WhenValidInputsAreGiven_Validator_ShouldNotReturnError"};

        // Act
        UpdateDirectorCommandValidator validator = new UpdateDirectorCommandValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().Be(0);   
    }

}
