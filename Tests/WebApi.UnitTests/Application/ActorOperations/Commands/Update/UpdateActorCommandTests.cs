using AutoMapper;
using FluentAssertions;
using WebApi.Application.ActorOperations.Commands.UpdateActor;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.ActorOperations.Commands.Update;

public class UpdateActorCommandTests : IClassFixture<CommonTestFixture>
{   
    private readonly MovieStoreDbContext context;
    private readonly IMapper mapper;

    public UpdateActorCommandTests(CommonTestFixture testFixture)
    {
        mapper = testFixture.Mapper;
        context = testFixture.Context;
    }

    [Fact]
    public void WhenGivenActorIdDoesNotExistInDb_InvalidOperationException_ShouldBeReturn()
    {
        // Arrange
        var command = new UpdateActorCommand(context, mapper);
        command.ActorId = -1;

        // Act - Assert
        FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("ActorId: " + command.ActorId + " does not exist.");
    }

    [Fact]
    public void WhenGivenActorNameAlreadyExistsInDb_InvalidOperationException_ShouldBeReturn()
    {
        // Arrange
        var actorInDb = new Actor{ 
                        Name = "WhenGivenActorNameAlreadyExistsInDb_InvalidOperationException_ShouldBeReturn1", 
                        Surname = "WhenGivenActorNameAlreadyExistsInDb_InvalidOperationException_ShouldBeReturn1"};

        var actorUpdating = new Actor{ 
                        Name = "WhenGivenActorNameAlreadyExistsInDb_InvalidOperationException_ShouldBeReturn2", 
                        Surname = "WhenGivenActorNameAlreadyExistsInDb_InvalidOperationException_ShouldBeReturn2"};

        context.Actors.Add(actorInDb);
        context.Actors.Add(actorUpdating);
        context.SaveChanges();

        var command = new UpdateActorCommand(context,mapper);
        command.ActorId = actorUpdating.Id;
        command.Model = new UpdateActorModel{
                            Name = actorInDb.Name,
                            Surname = actorInDb.Surname};

        // Act - Assert
        FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("ActorNameSurname: "+ command.Model.Name+" "+ command.Model.Surname+" already exists, choose another name.");
    }

    [Fact]
    public void WhenGivenActorIdExistsInDb_Actor_ShouldBeUpdated()
    {
        // Arrange
        var actorInDb = new Actor{ 
                        Name = "WhenGivenActorIdExistsInDb_Actor_ShouldBeUpdated", 
                        Surname = "WhenGivenActorIdExistsInDb_Actor_ShouldBeUpdated"};

        var actorCompared = new Actor{ 
                            Name = actorInDb.Name,
                            Surname = actorInDb.Surname};

        context.Actors.Add(actorInDb);
        context.SaveChanges();

        var command = new UpdateActorCommand(context,mapper);
        command.ActorId = actorInDb.Id;
        command.Model = new UpdateActorModel{
                            Name = "WhenGivenActorIdExistsInDb_Actor_ShouldBeUpdated2", 
                            Surname = "WhenGivenActorIdExistsInDb_Actor_ShouldBeUpdated2"};
        // Act
        FluentActions.Invoking(() => command.Handle()).Invoke();

        // Assert
        var actorUpdated = context.Actors.SingleOrDefault(b=> b.Id == actorInDb.Id);
        actorUpdated.Should().NotBeNull();
        actorUpdated.Name.Should().NotBe(actorCompared.Name);
        actorUpdated.Surname.Should().NotBe(actorCompared.Surname);

    }
}