using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.CustomerMoviesOperations.Queries.GetCustomerMovies;
using WebApi.DbOperations;

namespace WebApi.Controllers;

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
    public IActionResult GetCustomerMovies(int id)
    {
        var query = new GetCustomerMoviesQuery(context,mapper);
        query.CustomerId = id;

        var validator = new GetCustomerMoviesQueryValidator();
        validator.ValidateAndThrow(query);

        var customerMovies = query.Handle();

        return Ok(customerMovies);
    }
}