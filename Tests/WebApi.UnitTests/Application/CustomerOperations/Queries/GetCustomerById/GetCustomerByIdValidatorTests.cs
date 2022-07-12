using FluentAssertions;
using WebApi.Application.CustomerOperations.Queries.GetCustomerById;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.CustomerOperations.Queries.GetCustomerById;

public class GetCustomerByIdQueryValidatorTests : IClassFixture<CommonTestFixture>
{
    [Fact]
    public void WhenGivenCustomerIdIsNotGreaterThenZero_Validator_ShouldBeReturnError()
    {
        // Arrange
        GetCustomerByIdQuery command = new GetCustomerByIdQuery(null, null);
        command.CustomerId = 0;

        // Act
        GetCustomerByIdQueryValidator validator = new GetCustomerByIdQueryValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().BeGreaterThan(0);        
    }

    [Fact]
    public void WhenGivenCustomerIdIsGreaterThenZero__Validator_ShouldNotReturnError()
    {
        // Arrange
        GetCustomerByIdQuery command = new GetCustomerByIdQuery(null, null);
        command.CustomerId = 1;
        
        // Act
        GetCustomerByIdQueryValidator validator = new GetCustomerByIdQueryValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().Be(0);   
    }

}
