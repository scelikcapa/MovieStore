using AutoMapper;
using FluentAssertions;
using WebApi.Application.ActorOperations.Queries.GetActorById;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.ActorOperations.Queries.GetActorById;

public class GetActorByIdQueryTests : IClassFixture<CommonTestFixture> 
{
    private readonly MovieStoreDbContext context;
    private readonly IMapper mapper;

    public GetActorByIdQueryTests(CommonTestFixture testFixture)
    {
        context = testFixture.Context;
        mapper = testFixture.Mapper;
    }

     [Fact]
    public void WhenGivenActorIdDoesNotExistInDb_InvalidOperationException_ShouldBeReturn()
    {
        // arrange
        var actor = new Actor{ 
            Name = "WhenGivenActorIdDoesNotExistInDb_InvalidOperationException_ShouldBeReturn", 
            Surname = "WhenGivenActorIdDoesNotExistInDb_InvalidOperationException_ShouldBeReturn"};

        context.Actors.Add(actor);
        context.SaveChanges();
        
        context.Actors.Remove(actor);
        context.SaveChanges();

        GetActorByIdQuery command = new GetActorByIdQuery(context, mapper);
        command.ActorId= actor.Id;

        // act - assert
        FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("ActorId: " + command.ActorId + " does not exist.");
    }

    [Fact]
    public void WhenGivenActorIdDoesExistInDb_Actor_ShouldBeReturned()
    {
        // Arrange
        var actor = new Actor{ 
            Name = "WhenGivenActorIdDoesExistInDb_Actor_ShouldBeReturned", 
            Surname = "WhenGivenActorIdDoesExistInDb_Actor_ShouldBeReturned"};
        context.Actors.Add(actor);
        context.SaveChanges();

        GetActorByIdQuery command = new GetActorByIdQuery(context, mapper);
        command.ActorId = actor.Id;

        // Act
        var actorReturned = FluentActions.Invoking(() => command.Handle()).Invoke();

        // Assert
        actorReturned.Should().NotBeNull();
        actorReturned.Id.Should().Be(command.ActorId);
        actorReturned.Name.Should().Be(actor.Name);
        actorReturned.Surname.Should().Be(actor.Surname);
    }
}