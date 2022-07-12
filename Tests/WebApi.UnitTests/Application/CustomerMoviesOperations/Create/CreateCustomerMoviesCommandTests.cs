using AutoMapper;
using FluentAssertions;
using WebApi.Application.CustomerMoviesOperations.Commands.CreateCustomerMovies;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.CustomerMoviesOperations.Commands.Create;

public class CreateCustomerMoviesCommandTests : IClassFixture<CommonTestFixture>
{   
    private readonly MovieStoreDbContext context;
    private readonly IMapper mapper;

    public CreateCustomerMoviesCommandTests(CommonTestFixture testFixture)
    {
        mapper = testFixture.Mapper;
        context = testFixture.Context;
    }

    [Fact]
    public void WhenGivenCustomerIdDoesNotExistsInDb_InvalidOperationException_ShouldBeReturn()
    {
        // Arrange
        var command = new CreateCustomerMoviesCommand(context,mapper);
        command.CustomerId = -1;;

        // Act - Assert
        FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("CustomerId: " + command.CustomerId + " does not exist.");
    }

    [Fact]
    public void WhenGivenMovieIdDoesNotExistsInDb_InvalidOperationException_ShouldBeReturn()
    {
        // Arrange
        var command = new CreateCustomerMoviesCommand(context,mapper);
        command.CustomerId = 1;
        command.Model = new CreateCustomerMoviesModel{MovieId = -1};

        // Act - Assert
       FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("MovieId:" + command.Model.MovieId + " is not found.");
    }
}