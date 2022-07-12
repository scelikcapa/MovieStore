using FluentAssertions;
using WebApi.Application.MovieOperations.Commands.CreateMovie;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.MovieOperations.Commands.Create;

public class CreateMovieCommandValidatorTests : IClassFixture<CommonTestFixture>
{
    [Theory]
    [InlineData(null, 10, 1, 1)]
    [InlineData("", 10, 1, 1)]
    [InlineData("Ti", 10, 1, 1)]
    [InlineData("Title", 0, 1, 1)]
    [InlineData("Title", 1, 0, 1)]
    [InlineData("Title", 1, 1, 0)]
    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title, double price, int genreId, int directorId)
    {
        // Arrange
        CreateMovieCommand command = new CreateMovieCommand(null, null);
        command.Model = new CreateMovieModel{
            Title = title, 
            Year = 1991, 
            Price = price, 
            GenreId = genreId, 
            DirectorId = directorId};

        // Act
        CreateMovieCommandValidator validator = new CreateMovieCommandValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().BeInRange(1,2);      
    }

    [Fact]
    public void WhenDateTimeGreaterThenNowIsGiven_Validator_ShouldReturnError()
    {
        // Arrange
        CreateMovieCommand command = new CreateMovieCommand(null, null);
        command.Model = new CreateMovieModel{
            Title = "CreatingMovie",
            Year = DateTime.Now.Year + 1,
            Price = 10,
            GenreId = 1,
            DirectorId = 1
        };
        // Act
        CreateMovieCommandValidator validator = new CreateMovieCommandValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().BeGreaterThan(0);   
    }

    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
    {
        // Arrange
        CreateMovieCommand command = new CreateMovieCommand(null, null);
        command.Model = new CreateMovieModel{
            Title = "CreatingMovie",
            Year = DateTime.Now.Year,
            Price = 10,
            GenreId = 1,
            DirectorId = 1
        };
        // Act
        CreateMovieCommandValidator validator = new CreateMovieCommandValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().Be(0);   
    }

}
