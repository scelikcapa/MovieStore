using FluentAssertions;
using WebApi.Application.DirectorOperations.Commands.DeleteDirector;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.DirectorOperations.Commands.Delete;

public class DeleteDirectorCommandTests : IClassFixture<CommonTestFixture>
{   
    private readonly MovieStoreDbContext context;

    public DeleteDirectorCommandTests(CommonTestFixture testFixture)
    {
        context = testFixture.Context;
    }

    [Fact]
    public void WhenGivenDirectorIdDoesNotExistInDb_InvalidOperationException_ShouldBeReturn()
    {
        // Arrange
        var command = new DeleteDirectorCommand(context);
        command.DirectorId = -1;

        // Act - Assert
        FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("DirectorId: " + command.DirectorId + " does not exists.");
    }

    [Fact]
    public void WhenGivenDirectorHasRelation_InvalidOperationException_ShouldBeReturn()
    {
        // Arrange
        var movieInDb = new Movie{
                        Title = "WhenGivenDirectorHasRelation_InvalidOperationException_ShouldBeReturn", 
                        Year = new DateTime(1990,01,01), 
                        Price = 20, 
                        GenreId = 1, 
                        DirectorId = 1};

        context.Movies.Add(movieInDb);                
        context.SaveChanges();

        var directorInDb = new Director{
                        Name = "WhenGivenDirectorHasRelation_InvalidOperationException_ShouldBeReturn", 
                        Surname = "WhenGivenDirectorHasRelation_InvalidOperationException_ShouldBeReturn",
                        Movies = new List<Movie>{movieInDb}};

        context.Directors.Add(directorInDb);
        context.SaveChanges();

        var command = new DeleteDirectorCommand(context);
        command.DirectorId = directorInDb.Id;

        // Act - Assert
        FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("DirectorId: " + command.DirectorId + " has " +directorInDb.Movies.Count()+ " movies. Please delete them first.");
    }

    [Fact]
    public void WhenGivenDirectorHasNotRelation_Director_ShouldBeDeleted()
    {
        // Arrange
        var directorInDb = new Director{
                        Name = "WhenGivenDirectorHasNotRelation_Director_ShouldBeDeleted", 
                        Surname = "WhenGivenDirectorHasNotRelation_Director_ShouldBeDeleted",
                        Movies = null};

        context.Directors.Add(directorInDb);
        context.SaveChanges();

        var command = new DeleteDirectorCommand(context);
        command.DirectorId = directorInDb.Id;

        // Act
        FluentActions.Invoking(() => command.Handle()).Invoke();

        // Assert
        var director = context.Directors.SingleOrDefault(b=> b.Id == command.DirectorId);
        director.Should().BeNull();
    }
}