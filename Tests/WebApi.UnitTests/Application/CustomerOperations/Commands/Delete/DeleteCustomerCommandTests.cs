using FluentAssertions;
using WebApi.Application.CustomerOperations.Commands.DeleteCustomer;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.CustomerOperations.Commands.Delete;

public class DeleteCustomerCommandTests : IClassFixture<CommonTestFixture>
{   
    private readonly MovieStoreDbContext context;

    public DeleteCustomerCommandTests(CommonTestFixture testFixture)
    {
        context = testFixture.Context;
    }

    [Fact]
    public void WhenGivenCustomerIdDoesNotExistInDb_InvalidOperationException_ShouldBeReturn()
    {
        // Arrange
        var command = new DeleteCustomerCommand(context);
        command.CustomerId = -1;

        // Act - Assert
        FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("CustomerId: " + command.CustomerId + " does not exists.");
    }

     [Fact]
    public void WhenGivenCustomerIdExistsInDb_Customer_ShouldBeDeleted()
    {
        // Arrange
        var customerInDb = new Customer{
                        Name = "WhenGivenCustomerIdExistsInDb_Customer_ShouldBeDeleted", 
                        Surname = "WhenGivenCustomerIdExistsInDb_Customer_ShouldBeDeleted"};
                        
        context.Customers.Add(customerInDb);
        context.SaveChanges();

        var command = new DeleteCustomerCommand(context);
        command.CustomerId = customerInDb.Id;

        // Act
        FluentActions.Invoking(() => command.Handle()).Invoke();

        // Assert
        var customer = context.Customers.Single(b=> b.Id == command.CustomerId);
        customer.IsActive.Should().BeFalse();
    }
}