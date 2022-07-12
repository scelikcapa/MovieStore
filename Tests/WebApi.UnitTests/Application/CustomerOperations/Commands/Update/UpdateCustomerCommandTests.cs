using AutoMapper;
using FluentAssertions;
using WebApi.Application.CustomerOperations.Commands.UpdateCustomer;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.CustomerOperations.Commands.Update;

public class UpdateCustomerCommandTests : IClassFixture<CommonTestFixture>
{   
    private readonly MovieStoreDbContext context;
    private readonly IMapper mapper;

    public UpdateCustomerCommandTests(CommonTestFixture testFixture)
    {
        mapper = testFixture.Mapper;
        context = testFixture.Context;
    }

    [Fact]
    public void WhenGivenCustomerIdDoesNotExistInDb_InvalidOperationException_ShouldBeReturn()
    {
        // Arrange
        var command = new UpdateCustomerCommand(context, mapper);
        command.CustomerId = -1;

        // Act - Assert
        FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("CustomerId: " + command.CustomerId + " does not exist.");
    }

    [Fact]
    public void WhenGivenCustomerNameAlreadyExistsInDb_InvalidOperationException_ShouldBeReturn()
    {
        // Arrange
        var customerInDb = new Customer{ 
                        Name = "WhenGivenCustomerNameAlreadyExistsInDb_InvalidOperationException_ShouldBeReturn1", 
                        Surname = "WhenGivenCustomerNameAlreadyExistsInDb_InvalidOperationException_ShouldBeReturn1"};

        var customerUpdating = new Customer{ 
                        Name = "WhenGivenCustomerNameAlreadyExistsInDb_InvalidOperationException_ShouldBeReturn2", 
                        Surname = "WhenGivenCustomerNameAlreadyExistsInDb_InvalidOperationException_ShouldBeReturn2"};

        context.Customers.Add(customerInDb);
        context.Customers.Add(customerUpdating);
        context.SaveChanges();

        var command = new UpdateCustomerCommand(context,mapper);
        command.CustomerId = customerUpdating.Id;
        command.Model = new UpdateCustomerModel{
                            Name = customerInDb.Name,
                            Surname = customerInDb.Surname};

        // Act - Assert
        FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("CustomerNameSurname: "+ command.Model.Name+" "+ command.Model.Surname+" already exists, choose another name.");
    }

    [Fact]
    public void WhenGivenCustomerIdExistsInDb_Customer_ShouldBeUpdated()
    {
        // Arrange
        var customerInDb = new Customer{ 
                        Name = "WhenGivenCustomerIdExistsInDb_Customer_ShouldBeUpdated", 
                        Surname = "WhenGivenCustomerIdExistsInDb_Customer_ShouldBeUpdated"};

        var customerCompared = new Customer{ 
                            Name = customerInDb.Name,
                            Surname = customerInDb.Surname};

        context.Customers.Add(customerInDb);
        context.SaveChanges();

        var command = new UpdateCustomerCommand(context,mapper);
        command.CustomerId = customerInDb.Id;
        command.Model = new UpdateCustomerModel{
                            Name = "WhenGivenCustomerIdExistsInDb_Customer_ShouldBeUpdated2", 
                            Surname = "WhenGivenCustomerIdExistsInDb_Customer_ShouldBeUpdated2"};
        // Act
        FluentActions.Invoking(() => command.Handle()).Invoke();

        // Assert
        var customerUpdated = context.Customers.SingleOrDefault(b=> b.Id == customerInDb.Id);
        customerUpdated.Should().NotBeNull();
        customerUpdated.Name.Should().NotBe(customerCompared.Name);
        customerUpdated.Surname.Should().NotBe(customerCompared.Surname);

    }
}