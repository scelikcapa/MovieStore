using AutoMapper;
using FluentAssertions;
using WebApi.Application.CustomerOperations.Queries.GetCustomerById;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.CustomerOperations.Queries.GetCustomerById;

public class GetCustomerByIdQueryTests : IClassFixture<CommonTestFixture> 
{
    private readonly MovieStoreDbContext context;
    private readonly IMapper mapper;

    public GetCustomerByIdQueryTests(CommonTestFixture testFixture)
    {
        context = testFixture.Context;
        mapper = testFixture.Mapper;
    }

     [Fact]
    public void WhenGivenCustomerIdDoesNotExistInDb_InvalidOperationException_ShouldBeReturn()
    {
        // arrange
        var customer = new Customer{ 
            Name = "WhenGivenCustomerIdDoesNotExistInDb_InvalidOperationException_ShouldBeReturn", 
            Surname = "WhenGivenCustomerIdDoesNotExistInDb_InvalidOperationException_ShouldBeReturn", 
            Email = "WhenGivenCustomerIdDoesNotExistInDb_InvalidOperationException_ShouldBeReturn", 
            Password = "WhenGivenCustomerIdDoesNotExistInDb_InvalidOperationException_ShouldBeReturn"};

        context.Customers.Add(customer);
        context.SaveChanges();
        
        customer.IsActive = false;
        context.SaveChanges();

        GetCustomerByIdQuery command = new GetCustomerByIdQuery(context, mapper);
        command.CustomerId= customer.Id;

        // act - assert
        FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("CustomerId: " + command.CustomerId + " does not exist.");
    }

    [Fact]
    public void WhenGivenCustomerIdDoesExistInDb_Customer_ShouldBeReturned()
    {
        // Arrange
        var customer = new Customer{ 
            Name = "WhenGivenCustomerIdDoesExistInDb_Customer_ShouldBeReturned", 
            Surname = "WhenGivenCustomerIdDoesExistInDb_Customer_ShouldBeReturned", 
            Email = "WhenGivenCustomerIdDoesExistInDb_Customer_ShouldBeReturned", 
            Password = "WhenGivenCustomerIdDoesExistInDb_Customer_ShouldBeReturned"};
        context.Customers.Add(customer);
        context.SaveChanges();

        GetCustomerByIdQuery command = new GetCustomerByIdQuery(context, mapper);
        command.CustomerId = customer.Id;

        // Act
        var customerReturned = FluentActions.Invoking(() => command.Handle()).Invoke();

        // Assert
        customerReturned.Should().NotBeNull();
        customerReturned.Id.Should().Be(command.CustomerId);
        customerReturned.Name.Should().Be(customer.Name);
        customerReturned.Surname.Should().Be(customer.Surname);
    }
}