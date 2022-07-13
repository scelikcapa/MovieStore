using AutoMapper;
using FluentAssertions;
using WebApi.Application.CustomerOperations.Queries.GetCustomerById;
using WebApi.DbOperations;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.CustomerOperations.Queries.GetCustomersGenres;

public class GetCustomerGenresQueryValidatorTests : IClassFixture<CommonTestFixture> 
{
    private readonly MovieStoreDbContext context;
    private readonly IMapper mapper;

    public GetCustomerGenresQueryValidatorTests(CommonTestFixture testFixture)
    {
        context = testFixture.Context;
        mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenGivenCustomerIdIsNotGreaterThenZero_Validator_ShouldBeReturnError()
    {
        // Arrange
        var command = new GetCustomerGenresQuery(null, null);
        command.CustomerId = 0;

        // Act
        var validator = new GetCustomerGenresQueryValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().BeGreaterThan(0);        
    }

    [Fact]
    public void WhenGivenCustomerIdIsGreaterThenZero__Validator_ShouldNotReturnError()
    {
        // Arrange
        var command = new GetCustomerGenresQuery(null, null);
        command.CustomerId = 1;
        
        // Act
        var validator = new GetCustomerGenresQueryValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().Be(0);   
    }

   

}