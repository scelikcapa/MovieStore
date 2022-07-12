using AutoMapper;
using FluentAssertions;
using WebApi.Application.DirectorOperations.Queries.GetDirectorById;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.DirectorOperations.Queries.GetDirectorById;

public class GetDirectorByIdQueryTests : IClassFixture<CommonTestFixture> 
{
    private readonly MovieStoreDbContext context;
    private readonly IMapper mapper;

    public GetDirectorByIdQueryTests(CommonTestFixture testFixture)
    {
        context = testFixture.Context;
        mapper = testFixture.Mapper;
    }

     [Fact]
    public void WhenGivenDirectorIdDoesNotExistInDb_InvalidOperationException_ShouldBeReturn()
    {
        // arrange
        var director = new Director{ 
            Name = "WhenGivenDirectorIdDoesNotExistInDb_InvalidOperationException_ShouldBeReturn", 
            Surname = "WhenGivenDirectorIdDoesNotExistInDb_InvalidOperationException_ShouldBeReturn"};

        context.Directors.Add(director);
        context.SaveChanges();
        
        context.Directors.Remove(director);
        context.SaveChanges();

        GetDirectorByIdQuery command = new GetDirectorByIdQuery(context, mapper);
        command.DirectorId= director.Id;

        // act - assert
        FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("DirectorId: " + command.DirectorId + " does not exist.");
    }

    [Fact]
    public void WhenGivenDirectorIdDoesExistInDb_Director_ShouldBeReturned()
    {
        // Arrange
        var director = new Director{ 
            Name = "WhenGivenDirectorIdDoesExistInDb_Director_ShouldBeReturned", 
            Surname = "WhenGivenDirectorIdDoesExistInDb_Director_ShouldBeReturned"};
        context.Directors.Add(director);
        context.SaveChanges();

        GetDirectorByIdQuery command = new GetDirectorByIdQuery(context, mapper);
        command.DirectorId = director.Id;

        // Act
        var directorReturned = FluentActions.Invoking(() => command.Handle()).Invoke();

        // Assert
        directorReturned.Should().NotBeNull();
        directorReturned.Id.Should().Be(command.DirectorId);
        directorReturned.Name.Should().Be(director.Name);
        directorReturned.Surname.Should().Be(director.Surname);
    }
}