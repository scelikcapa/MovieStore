using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.CustomerOperations.Commands.AddCustomerGenre;
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
        var customers = query.Handle();

        return Ok(customers);
    }

    [HttpGet("{id}")]
    public IActionResult GetCustomerById(int id)
    {
        var query = new GetCustomerByIdQuery(context,mapper);
        query.CustomerId = id;

        var validator = new GetCustomerByIdQueryValidator();
        validator.ValidateAndThrow(query);

        var customer = query.Handle();

        return Ok(customer);
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
        var command = new DeleteCustomerCommand(context);
        command.CustomerId = id;

        var validator = new DeleteCustomerCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok();
    }

    [HttpGet("{id}/Genres")]
    public IActionResult GetCustomerGenres(int id)
    {
        var query = new GetCustomerGenresQuery(context,mapper);
        query.CustomerId = id;
        
        var validator = new GetCustomerGenresQueryValidator();
        validator.ValidateAndThrow(query);
        
        var customerGenres = query.Handle();

        return Ok(customerGenres);
    }

    [HttpPost("{id}/Genres")]
    public IActionResult AddCustomerGenre(int id, [FromBody] AddCustomerGenreModel newGenre)
    {
        var command = new AddCustomerGenreCommand(context,mapper);
        command.CustomerId = id;
        command.Model = newGenre;
        
        var validator = new AddCustomerGenreCommandValidator();
        validator.ValidateAndThrow(command);
        
        command.Handle();

        return Ok();
    }
}