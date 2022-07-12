using FluentAssertions;
using WebApi.Application.MovieOperations.Commands.DeleteMovie;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.MovieOperations.Commands.Delete;

public class DeleteMovieCommandValidatorTests : IClassFixture<CommonTestFixture>
{
    [Fact]
    public void WhenGivenMovieIdIsNotGreaterThenZero_Validator_ShouldReturnError()
    {
        // Arrange
        DeleteMovieCommand command = new DeleteMovieCommand(null);
        command.MovieId = 0;

        // Act
        DeleteMovieCommandValidator validator = new DeleteMovieCommandValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().BeGreaterThan(0);        
    }

    [Fact]
    public void WhenGivenMovieIdIsGreaterThenZero_Validator_ShouldNotReturnError()
    {
        // Arrange
        DeleteMovieCommand command = new DeleteMovieCommand(null);
        command.MovieId = 1;
        
        // Act
        DeleteMovieCommandValidator validator = new DeleteMovieCommandValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().Be(0);   
    }

}
