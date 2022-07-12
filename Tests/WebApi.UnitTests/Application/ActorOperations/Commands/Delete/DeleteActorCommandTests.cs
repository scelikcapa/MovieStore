using FluentAssertions;
using WebApi.Application.ActorOperations.Commands.DeleteActor;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.ActorOperations.Commands.Delete;

public class DeleteActorCommandTests : IClassFixture<CommonTestFixture>
{   
    private readonly MovieStoreDbContext context;

    public DeleteActorCommandTests(CommonTestFixture testFixture)
    {
        context = testFixture.Context;
    }

    [Fact]
    public void WhenGivenActorIdDoesNotExistInDb_InvalidOperationException_ShouldBeReturn()
    {
        // Arrange
        var command = new DeleteActorCommand(context);
        command.ActorId = -1;

        // Act - Assert
        FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("ActorId: " + command.ActorId + " does not exists.");
    }

    [Fact]
    public void WhenGivenActorHasRelation_InvalidOperationException_ShouldBeReturn()
    {
        // Arrange
        var movieInDb = new Movie{
                        Title = "WhenGivenActorHasRelation_InvalidOperationException_ShouldBeReturn", 
                        Year = new DateTime(1990,01,01), 
                        Price = 20, 
                        GenreId = 1, 
                        DirectorId = 1};

        context.Movies.Add(movieInDb);                
        context.SaveChanges();

        var actorInDb = new Actor{
                        Name = "WhenGivenActorHasRelation_InvalidOperationException_ShouldBeReturn", 
                        Surname = "WhenGivenActorHasRelation_InvalidOperationException_ShouldBeReturn",
                        Movies = new List<Movie>{movieInDb}};

        context.Actors.Add(actorInDb);
        context.SaveChanges();

        var command = new DeleteActorCommand(context);
        command.ActorId = actorInDb.Id;

        // Act - Assert
        FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("ActorId: " + command.ActorId + " has " +actorInDb.Movies.Count()+ " movies. Please delete them first.");
    }

    [Fact]
    public void WhenGivenActorHasNotRelation_Actor_ShouldBeDeleted()
    {
        // Arrange
        var actorInDb = new Actor{
                        Name = "WhenGivenActorHasNotRelation_Actor_ShouldBeDeleted", 
                        Surname = "WhenGivenActorHasNotRelation_Actor_ShouldBeDeleted",
                        Movies = null};

        context.Actors.Add(actorInDb);
        context.SaveChanges();

        var command = new DeleteActorCommand(context);
        command.ActorId = actorInDb.Id;

        // Act
        FluentActions.Invoking(() => command.Handle()).Invoke();

        // Assert
        var actor = context.Actors.SingleOrDefault(b=> b.Id == command.ActorId);
        actor.Should().BeNull();
    }
}