using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.CustomerMoviesOperations.Commands.CreateCustomerMovies;
using WebApi.Application.CustomerMoviesOperations.Queries.GetCustomerMoviesById;
using WebApi.DbOperations;

namespace WebApi.Controllers;

[Authorize]
[ApiController]
[Route("Customers")]
public class CustomerMoviesController : ControllerBase
{
    private readonly IMovieStoreDbContext context;
    private readonly IMapper mapper;

    public CustomerMoviesController(IMovieStoreDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    [HttpGet("{id}/Movies")]
    public IActionResult GetCustomerMoviesById(int id)
    {
        var query = new GetCustomerMoviesByIdQuery(context,mapper);
        query.CustomerId = id;

        var validator = new GetCustomerMoviesByIdQueryValidator();
        validator.ValidateAndThrow(query);

        var customerMovies = query.Handle();

        return Ok(customerMovies);
    }

    [HttpPost]
    [Route("{id}/Movies")]
    public IActionResult CreateCustomerMovies(int id, [FromBody] CreateCustomerMoviesModel purchasedMovie)
    {
        var commmand = new CreateCustomerMoviesCommand(context,mapper);
        commmand.CustomerId = id;
        commmand.Model = purchasedMovie;

        var validator = new CreateCustomerMoviesCommandValidator();
        validator.ValidateAndThrow(commmand);

        commmand.Handle();

        return Ok();
    }
}