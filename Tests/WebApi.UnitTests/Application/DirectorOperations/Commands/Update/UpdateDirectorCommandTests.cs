using AutoMapper;
using FluentAssertions;
using WebApi.Application.DirectorOperations.Commands.UpdateDirector;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.DirectorOperations.Commands.Update;

public class UpdateDirectorCommandTests : IClassFixture<CommonTestFixture>
{   
    private readonly MovieStoreDbContext context;
    private readonly IMapper mapper;

    public UpdateDirectorCommandTests(CommonTestFixture testFixture)
    {
        mapper = testFixture.Mapper;
        context = testFixture.Context;
    }

    [Fact]
    public void WhenGivenDirectorIdDoesNotExistInDb_InvalidOperationException_ShouldBeReturn()
    {
        // Arrange
        var command = new UpdateDirectorCommand(context, mapper);
        command.DirectorId = -1;

        // Act - Assert
        FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("DirectorId: " + command.DirectorId + " does not exist.");
    }

    [Fact]
    public void WhenGivenDirectorNameAlreadyExistsInDb_InvalidOperationException_ShouldBeReturn()
    {
        // Arrange
        var directorInDb = new Director{ 
                        Name = "WhenGivenDirectorNameAlreadyExistsInDb_InvalidOperationException_ShouldBeReturn1", 
                        Surname = "WhenGivenDirectorNameAlreadyExistsInDb_InvalidOperationException_ShouldBeReturn1"};

        var directorUpdating = new Director{ 
                        Name = "WhenGivenDirectorNameAlreadyExistsInDb_InvalidOperationException_ShouldBeReturn2", 
                        Surname = "WhenGivenDirectorNameAlreadyExistsInDb_InvalidOperationException_ShouldBeReturn2"};

        context.Directors.Add(directorInDb);
        context.Directors.Add(directorUpdating);
        context.SaveChanges();

        var command = new UpdateDirectorCommand(context,mapper);
        command.DirectorId = directorUpdating.Id;
        command.Model = new UpdateDirectorModel{
                            Name = directorInDb.Name,
                            Surname = directorInDb.Surname};

        // Act - Assert
        FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("DirectorNameSurname: "+ command.Model.Name+" "+ command.Model.Surname+" already exists, choose another name.");
    }

    [Fact]
    public void WhenGivenDirectorIdExistsInDb_Director_ShouldBeUpdated()
    {
        // Arrange
        var directorInDb = new Director{ 
                        Name = "WhenGivenDirectorIdExistsInDb_Director_ShouldBeUpdated", 
                        Surname = "WhenGivenDirectorIdExistsInDb_Director_ShouldBeUpdated"};

        var directorCompared = new Director{ 
                            Name = directorInDb.Name,
                            Surname = directorInDb.Surname};

        context.Directors.Add(directorInDb);
        context.SaveChanges();

        var command = new UpdateDirectorCommand(context,mapper);
        command.DirectorId = directorInDb.Id;
        command.Model = new UpdateDirectorModel{
                            Name = "WhenGivenDirectorIdExistsInDb_Director_ShouldBeUpdated2", 
                            Surname = "WhenGivenDirectorIdExistsInDb_Director_ShouldBeUpdated2"};
        // Act
        FluentActions.Invoking(() => command.Handle()).Invoke();

        // Assert
        var directorUpdated = context.Directors.SingleOrDefault(b=> b.Id == directorInDb.Id);
        directorUpdated.Should().NotBeNull();
        directorUpdated.Name.Should().NotBe(directorCompared.Name);
        directorUpdated.Surname.Should().NotBe(directorCompared.Surname);

    }
}