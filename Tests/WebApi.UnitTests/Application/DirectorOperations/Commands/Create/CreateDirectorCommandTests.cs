using AutoMapper;
using FluentAssertions;
using WebApi.Application.DirectorOperations.Commands.CreateDirector;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.DirectorOperations.Commands.Create;

public class CreateDirectorCommandTests : IClassFixture<CommonTestFixture>
{   
    private readonly MovieStoreDbContext context;
    private readonly IMapper mapper;

    public CreateDirectorCommandTests(CommonTestFixture testFixture)
    {
        mapper = testFixture.Mapper;
        context = testFixture.Context;
    }

    [Fact]
    public void WhenGivenDirectorNameAlreadyExistsInDb_InvalidOperationException_ShouldBeReturn()
    {
        // Arrange
        var directorInDb = new Director{ 
                        Name = "WhenGivenDirectorNameAlreadyExistsInDb_InvalidOperationException_ShouldBeReturn", 
                        Surname = "WhenGivenDirectorNameAlreadyExistsInDb_InvalidOperationException_ShouldBeReturn" };

        context.Directors.Add(directorInDb);
        context.SaveChanges();

        var command = new CreateDirectorCommand(context,mapper);
        command.Model = new CreateDirectorModel{
                            Name = directorInDb.Name,
                            Surname = directorInDb.Surname};

        // Act - Assert
        FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("DirectorNameSurname: " + command.Model.Name+" "+command.Model.Surname + " already exists.");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Director_ShouldBeCreated()
    {
        // Arrange
        var command = new CreateDirectorCommand(context,mapper);
        command.Model = new CreateDirectorModel{
                            Name = "WhenValidInputsAreGiven_Director_ShouldBeCreated", 
                            Surname = "WhenValidInputsAreGiven_Director_ShouldBeCreated"};

        // Act
        FluentActions.Invoking(() => command.Handle()).Invoke();

        // Assert
        var directorCreated = context.Directors.SingleOrDefault(b=> b.Name == command.Model.Name && b.Surname == command.Model.Surname);
        directorCreated.Should().NotBeNull();
        directorCreated.Name.Should().Be(command.Model.Name);
        directorCreated.Surname.Should().Be(command.Model.Surname);
    }
}