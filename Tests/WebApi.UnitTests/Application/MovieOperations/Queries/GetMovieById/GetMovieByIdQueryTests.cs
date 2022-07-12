using AutoMapper;
using FluentAssertions;
using WebApi.Application.MovieOperations.Queries.GetMovieById;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.MovieOperations.Queries.GetMovieById;

public class GetMovieByIdQueryTests : IClassFixture<CommonTestFixture> 
{
    private readonly MovieStoreDbContext context;
    private readonly IMapper mapper;

    public GetMovieByIdQueryTests(CommonTestFixture testFixture)
    {
        context = testFixture.Context;
        mapper = testFixture.Mapper;
    }

     [Fact]
    public void WhenGivenMovieIdDoesNotExistInDb_InvalidOperationException_ShouldBeReturn()
    {
        // arrange
        var movie = new Movie{ 
            Title = "WhenGivenMovieIdDoesNotExistInDb_InvalidOperationException_ShouldBeReturn", 
            Year = new DateTime(1990,01,01), 
            Price = 20, 
            GenreId = 1, 
            DirectorId = 1};
        context.Movies.Add(movie);
        context.SaveChanges();
        
        context.Movies.Remove(movie);
        context.SaveChanges();

        GetMovieByIdQuery command = new GetMovieByIdQuery(context, mapper);
        command.MovieId= movie.Id;

        // act - assert
        FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("MovieId: " + command.MovieId + " does not exist.");
    }

    [Fact]
    public void WhenGivenMovieIdDoesExistInDb_Movie_ShouldBeReturned()
    {
        // Arrange
        var movie = new Movie{ 
            Title = "WhenGivenMovieIdDoesExistInDb_Movie_ShouldBeRetuned", 
            Year = new DateTime(1990,01,01), 
            Price = 20, 
            GenreId = 1, 
            DirectorId = 1};
        context.Movies.Add(movie);
        context.SaveChanges();

        GetMovieByIdQuery command = new GetMovieByIdQuery(context, mapper);
        command.MovieId = movie.Id;

        // Act
        var movieReturned = FluentActions.Invoking(() => command.Handle()).Invoke();

        // Assert
        movieReturned.Should().NotBeNull();
        movieReturned.Id.Should().Be(command.MovieId);
        movieReturned.Title.Should().Be(movie.Title);
        movieReturned.Year.Should().Be(movie.Year.Year);
        movieReturned.Price.Should().Be(movie.Price);
        movieReturned.GenreId.Should().Be(movie.GenreId);
        movieReturned.DirectorId.Should().Be(movie.DirectorId);
    }
}