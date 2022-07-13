using AutoMapper;
using FluentAssertions;
using WebApi.Application.CustomerOperations.Queries.GetCustomerById;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.CustomerOperations.Queries.GetCustomersGenres;

public class GetCustomerGenresQueryTests : IClassFixture<CommonTestFixture> 
{
    private readonly MovieStoreDbContext context;
    private readonly IMapper mapper;

    public GetCustomerGenresQueryTests(CommonTestFixture testFixture)
    {
        context = testFixture.Context;
        mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenGivenCustomerIdDoesNotExistInDb_InvalidOperationException_ShouldBeReturn()
    {
        // Arrange
        var customer = new Customer{ 
            Name = "WhenGivenCustomerIdDoesNotExistInDb_InvalidOperationException_ShouldBeReturn", 
            Surname = "WhenGivenCustomerIdDoesNotExistInDb_InvalidOperationException_ShouldBeReturn"};

        context.Customers.Add(customer);
        context.SaveChanges();
        
        customer.IsActive = false;
        context.SaveChanges();

        var command = new GetCustomerGenresQuery(context, mapper);
        command.CustomerId= customer.Id;

        // Act - Assert
        FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("CustomerId: " + command.CustomerId + " does not exist.");
    }

    [Fact]
    public void WhenGivenCustomerDoesNotHaveGenres_InvalidOperationException_ShouldBeReturn()
    {
        // Arrange
        var customer = new Customer{ 
            Name = "WhenGivenCustomerDoesNotHaveGenres_InvalidOperationException_ShouldBeReturn", 
            Surname = "WhenGivenCustomerDoesNotHaveGenres_InvalidOperationException_ShouldBeReturn",
            Genres = null};

        context.Customers.Add(customer);
        context.SaveChanges();
        
        var command = new GetCustomerGenresQuery(context, mapper);
        command.CustomerId= customer.Id;

        // Act - Assert
        FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("CustomerId: " + command.CustomerId + " does not have any genre.");
    }

    [Fact]
    public void WhenGivenCustomerHaveGenres_Genres_ShouldBeReturn()
    {
        // Arrange
        var genre = new Genre{ Name = "NewGenre"};
        var genreList = new List<Genre>{genre};

        var customer = new Customer{ 
            Name = "WhenGivenCustomerHaveGenres_Genres_ShouldBeReturn", 
            Surname = "WhenGivenCustomerHaveGenres_Genres_ShouldBeReturn",
            Genres = genreList};

        context.Customers.Add(customer);
        context.SaveChanges();
        
        var command = new GetCustomerGenresQuery(context, mapper);
        command.CustomerId= customer.Id;

        // Act
        var customerGenres = FluentActions.Invoking(() => command.Handle()).Invoke();

        // Assert  
        customerGenres.Should().HaveCount(1);
        customerGenres[0].Id.Should().Be(genre.Id);    
        customerGenres[0].Name.Should().Be(genre.Name);    
    }

}