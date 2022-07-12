using AutoMapper;
using FluentAssertions;
using WebApi.Application.CustomerMoviesOperations.Queries.GetCustomerMoviesById;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.CustomerMoviesOperations.Queries.GetCustomerMoviesById;

public class GetCustomerMoviesByIdQueryTests : IClassFixture<CommonTestFixture> 
{
    private readonly MovieStoreDbContext context;
    private readonly IMapper mapper;

    public GetCustomerMoviesByIdQueryTests(CommonTestFixture testFixture)
    {
        context = testFixture.Context;
        mapper = testFixture.Mapper;
    }

     [Fact]
    public void WhenGivenCustomerIdDoesNotExistInDb_InvalidOperationException_ShouldBeReturn()
    {
        // arrange
        var command = new GetCustomerMoviesByIdQuery(context, mapper);
        command.CustomerId = -1;

        // act - assert
        FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("CustomerId: " + command.CustomerId + " does not exist.");
    }

    [Fact]
    public void WhenGivenCustomerDoesNotHaveMovies_InvalidOperationException_ShouldBeReturned()
    {
        // arrange
        var customer = new Customer{ 
            Name = "WhenGivenCustomerMoviesIdDoesExistInDb_CustomerMovies_ShouldBeReturned", 
            Surname = "WhenGivenCustomerMoviesIdDoesExistInDb_CustomerMovies_ShouldBeReturned"};
        context.Customers.Add(customer);
        context.SaveChanges();

        var command = new GetCustomerMoviesByIdQuery(context, mapper);
        command.CustomerId = customer.Id;

        // act - assert
        FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("CustomerId: " + command.CustomerId + " does not have any movie.");
    }

    [Fact]
    public void WhenGivenCustomerHasMovie_CustomerMovies_ShouldBeReturned()
    {
        // arrange
        var movieInDb = new Movie{ 
                        Title = "WhenGivenCustomerHasMovie_CustomerMovies_ShouldBeReturned", 
                        Year = new DateTime(1990,01,01), 
                        Price = 20, 
                        GenreId = 1, 
                        DirectorId = 1};
        
        var customerInDb = new Customer{ 
            Name = "WhenGivenCustomerMoviesIdDoesExistInDb_CustomerMovies_ShouldBeReturned", 
            Surname = "WhenGivenCustomerMoviesIdDoesExistInDb_CustomerMovies_ShouldBeReturned"};

        context.Movies.Add(movieInDb);
        context.Customers.Add(customerInDb);
        context.SaveChanges();

        var customerMovieInDb = new CustomerMovie{
                                    CustomerId = customerInDb.Id,
                                    MovieId = movieInDb.Id};
        
        context.CustomerMovies.Add(customerMovieInDb);
        context.SaveChanges();

        var command = new GetCustomerMoviesByIdQuery(context, mapper);
        command.CustomerId = customerInDb.Id;

        // act
        var customerMovies = FluentActions.Invoking(() => command.Handle()).Invoke();

        // assert
        customerMovies.Should().NotBeNull().And.HaveCount(1);
        customerMovies[0].Id.Should().Be(customerMovieInDb.Id);
        customerMovies[0].Price.Should().Be(customerMovieInDb.Price);
        customerMovies[0].OrderDate.Should().Be(customerMovieInDb.OrderDate.ToString("yyyy-MM-dd hh:mm:ss"));
        customerMovies[0].MovieId.Should().Be(customerMovieInDb.MovieId);
    }
}