using FluentAssertions;
using WebApi.Application.MovieOperations.Queries.GetMovieById;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.MovieOperations.Queries.GetMovieById;

public class GetMovieByIdQueryValidatorTests : IClassFixture<CommonTestFixture>
{
    [Fact]
    public void WhenGivenMovieIdIsNotGreaterThenZero_Validator_ShouldBeReturnError()
    {
        // Arrange
        GetMovieByIdQuery command = new GetMovieByIdQuery(null, null);
        command.MovieId = 0;

        // Act
        GetMovieByIdQueryValidator validator = new GetMovieByIdQueryValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().BeGreaterThan(0);        
    }

    [Fact]
    public void WhenGivenMovieIdIsGreaterThenZero__Validator_ShouldNotReturnError()
    {
        // Arrange
        GetMovieByIdQuery command = new GetMovieByIdQuery(null, null);
        command.MovieId = 1;
        
        // Act
        GetMovieByIdQueryValidator validator = new GetMovieByIdQueryValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().Be(0);   
    }

}
