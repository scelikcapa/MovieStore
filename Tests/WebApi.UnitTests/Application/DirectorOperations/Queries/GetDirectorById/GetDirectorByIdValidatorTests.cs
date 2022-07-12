using FluentAssertions;
using WebApi.Application.DirectorOperations.Queries.GetDirectorById;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.DirectorOperations.Queries.GetDirectorById;

public class GetDirectorByIdQueryValidatorTests : IClassFixture<CommonTestFixture>
{
    [Fact]
    public void WhenGivenDirectorIdIsNotGreaterThenZero_Validator_ShouldBeReturnError()
    {
        // Arrange
        GetDirectorByIdQuery command = new GetDirectorByIdQuery(null, null);
        command.DirectorId = 0;

        // Act
        GetDirectorByIdQueryValidator validator = new GetDirectorByIdQueryValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().BeGreaterThan(0);        
    }

    [Fact]
    public void WhenGivenDirectorIdIsGreaterThenZero__Validator_ShouldNotReturnError()
    {
        // Arrange
        GetDirectorByIdQuery command = new GetDirectorByIdQuery(null, null);
        command.DirectorId = 1;
        
        // Act
        GetDirectorByIdQueryValidator validator = new GetDirectorByIdQueryValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().Be(0);   
    }

}
