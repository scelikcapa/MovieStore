using AutoMapper;
using FluentAssertions;
using WebApi.Application.CustomerOperations.Commands.CreateCustomer;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.CustomerOperations.Commands.Create;

public class CreateCustomerCommandTests : IClassFixture<CommonTestFixture>
{   
    private readonly MovieStoreDbContext context;
    private readonly IMapper mapper;

    public CreateCustomerCommandTests(CommonTestFixture testFixture)
    {
        mapper = testFixture.Mapper;
        context = testFixture.Context;
    }
    
    [Fact]
    public void WhenGivenCustomerEmailAlreadyExistsInDb_InvalidOperationException_ShouldBeReturn()
    {
        // Arrange
        var customerInDb = new Customer{ 
                        Name = "WhenGivenCustomerEmailAlreadyExistsInDb_InvalidOperationException_ShouldBeReturn", 
                        Surname = "WhenGivenCustomerEmailAlreadyExistsInDb_InvalidOperationException_ShouldBeReturn",
                        Email = "WhenGivenCustomerEmailAlreadyExistsInDb_InvalidOperationException_ShouldBeReturn",
                        Password = "WhenGivenCustomerEmailAlreadyExistsInDb_InvalidOperationException_ShouldBeReturn" };

        context.Customers.Add(customerInDb);
        context.SaveChanges();

        var command = new CreateCustomerCommand(context,mapper);
        command.Model = new CreateCustomerModel{
                            Name = customerInDb.Name,
                            Surname = customerInDb.Surname,
                            Email = customerInDb.Email,
                            Password = customerInDb.Password};

        // Act - Assert
        FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("CustomerEmail: " + command.Model.Email + " already exists.");
    }

    [Fact]
    public void WhenGivenCustomerNameAlreadyExistsInDb_InvalidOperationException_ShouldBeReturn()
    {
        // Arrange
        var customerInDb = new Customer{ 
                        Name = "WhenGivenCustomerNameAlreadyExistsInDb_InvalidOperationException_ShouldBeReturn", 
                        Surname = "WhenGivenCustomerNameAlreadyExistsInDb_InvalidOperationException_ShouldBeReturn", 
                        Email = "WhenGivenCustomerNameAlreadyExistsInDb_InvalidOperationException_ShouldBeReturn", 
                        Password = "WhenGivenCustomerNameAlreadyExistsInDb_InvalidOperationException_ShouldBeReturn" };

        context.Customers.Add(customerInDb);
        context.SaveChanges();

        var command = new CreateCustomerCommand(context,mapper);
        command.Model = new CreateCustomerModel{
                                Name = customerInDb.Name,
                                Surname = customerInDb.Surname,
                                Email = "differentEmail",
                                Password = customerInDb.Password};

        // Act - Assert
        FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("CustomerNameSurname: " + command.Model.Name+" "+command.Model.Surname + " already exists.");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Customer_ShouldBeCreated()
    {
        // Arrange
        var command = new CreateCustomerCommand(context,mapper);
        command.Model = new CreateCustomerModel{
                            Name = "WhenValidInputsAreGiven_Customer_ShouldBeCreated", 
                            Surname = "WhenValidInputsAreGiven_Customer_ShouldBeCreated",
                            Email = "WhenValidInputsAreGiven_Customer_ShouldBeCreated",
                            Password = "WhenValidInputsAreGiven_Customer_ShouldBeCreated"};

        // Act
        FluentActions.Invoking(() => command.Handle()).Invoke();

        // Assert
        var customerCreated = context.Customers.SingleOrDefault(b=> b.Name == command.Model.Name && b.Surname == command.Model.Surname);
        customerCreated.Should().NotBeNull();
        customerCreated.Name.Should().Be(command.Model.Name);
        customerCreated.Surname.Should().Be(command.Model.Surname);
    }
}