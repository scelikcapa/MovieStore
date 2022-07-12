using FluentAssertions;
using WebApi.Application.MovieOperations.Commands.DeleteMovie;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.MovieOperations.Commands.Delete;

public class DeleteMovieCommandTests : IClassFixture<CommonTestFixture>
{   
    private readonly MovieStoreDbContext context;

    public DeleteMovieCommandTests(CommonTestFixture testFixture)
    {
        context = testFixture.Context;
    }

    [Fact]
    public void WhenGivenMovieIdDoesNotExistInDb_InvalidOperationException_ShouldBeReturn()
    {
        // Arrange
        var command = new DeleteMovieCommand(context);
        command.MovieId = -1;

        // Act - Assert
        FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Movie Id: " + command.MovieId + " does not exists.");
    }

    [Fact]
    public void WhenGivenMovieIdExistsInDb_Movie_ShouldBeDeleted()
    {
        // Arrange
        var movieInDb = new Movie{
                        Title = "WhenGivenMovieIdExistsInDb_Movie_ShouldBeDeleted", 
                        Year = new DateTime(1990,01,01), 
                        Price = 20, 
                        GenreId = 1, 
                        DirectorId = 1};
                        
        context.Movies.Add(movieInDb);
        context.SaveChanges();

        var command = new DeleteMovieCommand(context);
        command.MovieId = movieInDb.Id;

        // Act
        FluentActions.Invoking(() => command.Handle()).Invoke();

        // Assert
        var movie = context.Movies.SingleOrDefault(b=> b.Id == command.MovieId);
        movie.IsActive.Should().BeFalse();
    }
}