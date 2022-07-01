using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.CustomerOperations.Commands.CreateCustomer;
using WebApi.Application.CustomerOperations.Commands.DeleteCustomer;
using WebApi.Application.CustomerOperations.Commands.UpdateCustomer;
using WebApi.Application.CustomerOperations.Queries.GetCustomerById;
using WebApi.Application.CustomerOperations.Queries.GetCustomers;
using WebApi.DbOperations;

namespace WebApi.Controllers;

[ApiController]
[Route("[Controller]")]
public class CustomersController : ControllerBase
{
    private readonly IMapper mapper;
    private readonly IMovieStoreDbContext context;

    public CustomersController(IMovieStoreDbContext context, IMapper mapper)
    {
        this.mapper = mapper;
        this.context = context;
    }

     [HttpGet]
    public IActionResult GetCustomers()
    {
        var query = new GetCustomersQuery(context,mapper);
        var movies = query.Handle();

        return Ok(movies);
    }

    [HttpGet("{id}")]
    public IActionResult GetCustomerById(int id)
    {
        var query = new GetCustomerByIdQuery(context,mapper);
        query.CustomerId = id;

        var validator = new GetCustomerByIdQueryValidator();
        validator.ValidateAndThrow(query);

        var movie = query.Handle();

        return Ok(movie);
    }

    [HttpPost]
    public IActionResult CreateCustomer([FromBody] CreateCustomerModel newCustomer)
    {
        var command = new CreateCustomerCommand(context,mapper);
        command.Model=newCustomer;

        var validator = new CreateCustomerCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateCustomer(int id, [FromBody] UpdateCustomerModel updatedCustomer)
    {
        var command = new UpdateCustomerCommand(context,mapper);
        command.CustomerId = id;
        command.Model=updatedCustomer;

        var validator = new UpdateCustomerCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteCustomer(int id)
    {
        var command = new DeleteCustomerCommand(context,mapper);
        command.CustomerId = id;

        var validator = new DeleteCustomerCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok();
    }
}