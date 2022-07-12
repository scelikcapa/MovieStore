using AutoMapper;
using FluentAssertions;
using WebApi.Application.MovieOperations.Commands.CreateMovie;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.MovieOperations.Commands.Create;

public class CreateMovieCommandTests : IClassFixture<CommonTestFixture>
{   
    private readonly MovieStoreDbContext context;
    private readonly IMapper mapper;

    public CreateMovieCommandTests(CommonTestFixture testFixture)
    {
        mapper = testFixture.Mapper;
        context = testFixture.Context;
    }

    [Fact]
    public void WhenGivenMovieTitleAlreadyExistsInDb_InvalidOperationException_ShouldBeReturn()
    {
        // Arrange
        var movieInDb = new Movie{ 
                        Title = "WhenGivenMovieTitleAlreadyExistsInDb_InvalidOperationException_ShouldBeReturn", 
                        Year = new DateTime(1990,01,01), 
                        Price = 20, 
                        GenreId = 1, 
                        DirectorId = 1};

        context.Movies.Add(movieInDb);
        context.SaveChanges();

        var command = new CreateMovieCommand(context,mapper);
        command.Model = new CreateMovieModel{Title = movieInDb.Title};

        // Act - Assert
        FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("MovieTitle: " + command.Model.Title + " already exists, choose another name.");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Movie_ShouldBeCreated()
    {
        // Arrange
        var command = new CreateMovieCommand(context,mapper);
        command.Model = new CreateMovieModel{
                            Title = "WhenValidInputsAreGiven_Movie_ShouldBeCreated", 
                            Year = 1991, 
                            Price = 30, 
                            GenreId = 2, 
                            DirectorId = 2};
        // Act
        FluentActions.Invoking(() => command.Handle()).Invoke();

        // Assert
        var movieCreated = context.Movies.SingleOrDefault(b=> b.Title == command.Model.Title);
        movieCreated.Should().NotBeNull();
        movieCreated.Title.Should().Be(command.Model.Title);
        movieCreated.Year.Year.Should().Be(command.Model.Year);
        movieCreated.Price.Should().Be(command.Model.Price);
        movieCreated.GenreId.Should().Be(command.Model.GenreId);
        movieCreated.DirectorId.Should().Be(command.Model.DirectorId);

    }
}