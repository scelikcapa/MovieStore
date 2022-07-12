using FluentAssertions;
using WebApi.Application.ActorOperations.Queries.GetActorById;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.ActorOperations.Queries.GetActorById;

public class GetActorByIdQueryValidatorTests : IClassFixture<CommonTestFixture>
{
    [Fact]
    public void WhenGivenActorIdIsNotGreaterThenZero_Validator_ShouldBeReturnError()
    {
        // Arrange
        GetActorByIdQuery command = new GetActorByIdQuery(null, null);
        command.ActorId = 0;

        // Act
        GetActorByIdQueryValidator validator = new GetActorByIdQueryValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().BeGreaterThan(0);        
    }

    [Fact]
    public void WhenGivenActorIdIsGreaterThenZero__Validator_ShouldNotReturnError()
    {
        // Arrange
        GetActorByIdQuery command = new GetActorByIdQuery(null, null);
        command.ActorId = 1;
        
        // Act
        GetActorByIdQueryValidator validator = new GetActorByIdQueryValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().Be(0);   
    }

}
