using FluentAssertions;
using WebApi.Application.MovieOperations.Commands.UpdateMovie;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.MovieOperations.Commands.Update;

public class UpdateMovieCommandValidatorTests : IClassFixture<CommonTestFixture>
{
    [Theory]
    [InlineData(0, null, null, null, null)]
    [InlineData(1, "AI", null, null, null)]
    [InlineData(1, null, 0, null, null)]
    [InlineData(1, null, null, 0, null)]
    [InlineData(1, null, null, null, 0)]
    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int movieId, string title, double? price, int? genreId, int? directorId)    
    {
        // Arrange
        UpdateMovieCommand command = new UpdateMovieCommand(null, null);
        command.MovieId = movieId;
        command.Model = new UpdateMovieModel{
            Title = title,
            Price = price,
            GenreId = genreId,
            DirectorId = directorId
        };

        // Act
        UpdateMovieCommandValidator validator = new UpdateMovieCommandValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().Be(1);        
    }

    [Fact]
    public void WhenYearGreaterThenThisYearIsGiven_Validator_ShouldReturnError()
    {
        // Arrange
        UpdateMovieCommand command = new UpdateMovieCommand(null, null);
        command.MovieId = 1;
        command.Model = new UpdateMovieModel{
            Year = DateTime.Now.Year + 1
        };
        // Act
        UpdateMovieCommandValidator validator = new UpdateMovieCommandValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().Be(1);   
    }

    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
    {
        // Arrange
        UpdateMovieCommand command = new UpdateMovieCommand(null, null);
        command.MovieId = 1;
        command.Model = new UpdateMovieModel{
            Title = "title",
            Year = 2022,
            Price = 22,
            GenreId = 2,
            DirectorId = 2
        };
        // Act
        UpdateMovieCommandValidator validator = new UpdateMovieCommandValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().Be(0);   
    }

}
