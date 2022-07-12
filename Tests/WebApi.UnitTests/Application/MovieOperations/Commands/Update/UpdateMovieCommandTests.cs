using AutoMapper;
using FluentAssertions;
using WebApi.Application.MovieOperations.Commands.UpdateMovie;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.MovieOperations.Commands.Update;

public class UpdateMovieCommandTests : IClassFixture<CommonTestFixture>
{   
    private readonly MovieStoreDbContext context;
    private readonly IMapper mapper;

    public UpdateMovieCommandTests(CommonTestFixture testFixture)
    {
        mapper = testFixture.Mapper;
        context = testFixture.Context;
    }

    [Fact]
    public void WhenGivenMovieIdDoesNotExistInDb_InvalidOperationException_ShouldBeReturn()
    {
        // Arrange
        var command = new UpdateMovieCommand(context, mapper);
        command.MovieId = -1;

        // Act - Assert
        FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("MovieId: " + command.MovieId + " does not exist.");
    }

    [Fact]
    public void WhenGivenMovieTitleAlreadyExistsInDb_InvalidOperationException_ShouldBeReturn()
    {
        // Arrange
        var movieInDb = new Movie{ 
                        Title = "WhenGivenMovieTitleAlreadyExistsInDb_InvalidOperationException_ShouldBeReturn1", 
                        Year = new DateTime(1990,01,01), 
                        Price = 20, 
                        GenreId = 1, 
                        DirectorId = 1};

        var movieUpdating = new Movie{ 
                        Title = "WhenGivenMovieTitleAlreadyExistsInDb_InvalidOperationException_ShouldBeReturn2", 
                        Year = new DateTime(1990,01,01), 
                        Price = 20, 
                        GenreId = 1, 
                        DirectorId = 1};

        context.Movies.Add(movieInDb);
        context.Movies.Add(movieUpdating);
        context.SaveChanges();

        var command = new UpdateMovieCommand(context,mapper);
        command.MovieId = movieUpdating.Id;
        command.Model = new UpdateMovieModel{Title = movieInDb.Title};

        // Act - Assert
        FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("MovieTitle: "+ command.Model.Title +" already exists, choose another name.");
    }

    [Fact]
    public void WhenGivenMovieIdExistsInDb_Movie_ShouldBeUpdated()
    {
        // Arrange
        var movieInDb = new Movie{ 
                        Title = "WhenGivenMovieIdExistsInDb_Movie_ShouldBeUpdated1", 
                        Year = new DateTime(1990,01,01), 
                        Price = 20, 
                        GenreId = 1, 
                        DirectorId = 1};

        var movieCompared = new Movie{ 
                            Title = movieInDb.Title,
                            Year = movieInDb.Year, 
                            Price = movieInDb.Price, 
                            GenreId = movieInDb.GenreId, 
                            DirectorId = movieInDb.DirectorId};

        context.Movies.Add(movieInDb);
        context.SaveChanges();

        var command = new UpdateMovieCommand(context,mapper);
        command.MovieId = movieInDb.Id;
        command.Model = new UpdateMovieModel{
                            Title = "WhenGivenMovieIdExistsInDb_Movie_ShouldBeUpdated2", 
                            Year = 1991, 
                            Price = 30, 
                            GenreId = 2, 
                            DirectorId = 2};
        // Act
        FluentActions.Invoking(() => command.Handle()).Invoke();

        // Assert
        var movieUpdated = context.Movies.SingleOrDefault(b=> b.Id == movieInDb.Id);
        movieUpdated.Should().NotBeNull();
        movieUpdated.Title.Should().NotBe(movieCompared.Title);
        movieUpdated.Year.Should().NotBe(movieCompared.Year);
        movieUpdated.Price.Should().NotBe(movieCompared.Price);
        movieUpdated.GenreId.Should().NotBe(movieCompared.GenreId);
        movieUpdated.DirectorId.Should().NotBe(movieCompared.DirectorId);

    }
}