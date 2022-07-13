using AutoMapper;
using FluentAssertions;
using WebApi.Application.CustomerOperations.Commands.AddCustomerGenre;
using WebApi.DbOperations;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.CustomerOperations.Commands.AddCustomerGenres;

public class AddCustomerGenreCommandValidatorTests : IClassFixture<CommonTestFixture> 
{
    private readonly MovieStoreDbContext context;
    private readonly IMapper mapper;

    public AddCustomerGenreCommandValidatorTests(CommonTestFixture testFixture)
    {
        context = testFixture.Context;
        mapper = testFixture.Mapper;
    }

    [Theory]
    [InlineData(0, 1)]
    [InlineData(1, 0)]
    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int customerId, int genreId)    
    {
        // Arrange
        var command = new AddCustomerGenreCommand(null, null);
        command.CustomerId = customerId;
        command.Model = new AddCustomerGenreModel{GenreId = genreId};

        // Act
        var validator = new AddCustomerGenreCommandValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().Be(1);        
    }

    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
    {
        // Arrange
        var command = new AddCustomerGenreCommand(null, null);
        command.CustomerId = 1;
        command.Model = new AddCustomerGenreModel{GenreId = 1};

        // Act
        var validator = new AddCustomerGenreCommandValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().Be(0);   
    }



}