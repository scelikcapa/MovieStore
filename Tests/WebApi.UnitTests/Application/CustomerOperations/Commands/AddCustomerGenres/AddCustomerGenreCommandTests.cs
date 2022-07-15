using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WebApi.Application.CustomerOperations.Commands.AddCustomerGenre;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.CustomerOperations.Commands.AddCustomerGenres;

public class AddCustomerGenreCommandTests : IClassFixture<CommonTestFixture> 
{
    private readonly MovieStoreDbContext context;
    private readonly IMapper mapper;

    public AddCustomerGenreCommandTests(CommonTestFixture testFixture)
    {
        context = testFixture.Context;
        mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenGivenCustomerIdDoesNotExistInDb_InvalidOperationException_ShouldBeReturned()
    {
        // Arrange
        var command = new AddCustomerGenreCommand(context, mapper);
        command.CustomerId = -1;

        // Act - Assert
        FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("CustomerId: " + command.CustomerId + " does not exist.");
    }

    [Fact]
    public void WhenGivenGenreDoesNotExistsInDb_InvalidOperationException_ShouldBeReturned()
    {
        // Arrange
        var command = new AddCustomerGenreCommand(context, mapper);
        command.CustomerId = 1;
        command.Model = new AddCustomerGenreModel{GenreId = -1};

        // Act - Assert
        FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("GenreId: " + command.Model.GenreId + " not found.");
    }

    [Fact]
    public void WhenGivenGenreAlreadyAddedToCustomer_InvalidOperationException_ShouldBeReturned()
    {
        // Arrange
        var genreNew = new Genre{Name = "NewGenre"};
        context.Genres.Add(genreNew);
        context.Customers.Include(c => c.Genres).Single(c=>c.Id == 1).Genres.Add(genreNew);
        context.SaveChanges();

        var command = new AddCustomerGenreCommand(context, mapper);
        command.CustomerId = 1;
        command.Model = new AddCustomerGenreModel{GenreId = genreNew.Id};
        
        // Act - Assert
        FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Same GenreId:" + command.Model.GenreId + " already added to CustomerId: "+ command.CustomerId);

    }

    [Fact]
    public void WhenGivenGenreIsNotAddedToCustomer_CustomerGenre_ShouldBeAdded()
    {
        // Arrange
        var genreInDb = new Genre { Name = "WhenGivenGenreIsNotAddedToCustomer_CustomerGenre_ShouldBeAdded"};
        context.Genres.Add(genreInDb);
        context.SaveChanges();

        var command = new AddCustomerGenreCommand(context, mapper);
        command.CustomerId = 1;
        command.Model = new AddCustomerGenreModel{GenreId = genreInDb.Id};
        
        // Act
        FluentActions.Invoking(() => command.Handle()).Invoke();
                
                
        // Assert
        var customer = context.Customers.Include(c => c.Genres).Single(c=> c.Id == command.CustomerId);
        customer.Genres.Contains(genreInDb).Should().BeTrue();
    }


}