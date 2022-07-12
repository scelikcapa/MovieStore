using AutoMapper;
using FluentAssertions;
using WebApi.Application.ActorOperations.Commands.CreateActor;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.ActorOperations.Commands.Create;

public class CreateActorCommandTests : IClassFixture<CommonTestFixture>
{   
    private readonly MovieStoreDbContext context;
    private readonly IMapper mapper;

    public CreateActorCommandTests(CommonTestFixture testFixture)
    {
        mapper = testFixture.Mapper;
        context = testFixture.Context;
    }

    [Fact]
    public void WhenGivenActorNameAlreadyExistsInDb_InvalidOperationException_ShouldBeReturn()
    {
        // Arrange
        var actorInDb = new Actor{ 
                        Name = "WhenGivenActorNameAlreadyExistsInDb_InvalidOperationException_ShouldBeReturn", 
                        Surname = "WhenGivenActorNameAlreadyExistsInDb_InvalidOperationException_ShouldBeReturn" };

        context.Actors.Add(actorInDb);
        context.SaveChanges();

        var command = new CreateActorCommand(context,mapper);
        command.Model = new CreateActorModel{
                            Name = actorInDb.Name,
                            Surname = actorInDb.Surname};

        // Act - Assert
        FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("ActorNameSurname: " + command.Model.Name+" "+command.Model.Surname + " already exists.");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Actor_ShouldBeCreated()
    {
        // Arrange
        var command = new CreateActorCommand(context,mapper);
        command.Model = new CreateActorModel{
                            Name = "WhenValidInputsAreGiven_Actor_ShouldBeCreated", 
                            Surname = "WhenValidInputsAreGiven_Actor_ShouldBeCreated"};

        // Act
        FluentActions.Invoking(() => command.Handle()).Invoke();

        // Assert
        var actorCreated = context.Actors.SingleOrDefault(b=> b.Name == command.Model.Name && b.Surname == command.Model.Surname);
        actorCreated.Should().NotBeNull();
        actorCreated.Name.Should().Be(command.Model.Name);
        actorCreated.Surname.Should().Be(command.Model.Surname);
    }
}