using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.DirectorOperations.Commands.CreateDirector;
using WebApi.Application.DirectorOperations.Commands.DeleteDirector;
using WebApi.Application.DirectorOperations.Commands.UpdateDirector;
using WebApi.Application.DirectorOperations.Queries.GetDirectorById;
using WebApi.Application.DirectorOperations.Queries.GetDirectors;
using WebApi.DbOperations;

namespace WebApi.Controllers;

[ApiController]
[Route("[Controller]")]
public class DirectorsController : ControllerBase
{
    private readonly IMapper mapper;
    private readonly IMovieStoreDbContext context;

    public DirectorsController(IMovieStoreDbContext context, IMapper mapper)
    {
        this.mapper = mapper;
        this.context = context;
    }

     [HttpGet]
    public IActionResult GetDirectors()
    {
        var query = new GetDirectorsQuery(context,mapper);
        var movies = query.Handle();

        return Ok(movies);
    }

    [HttpGet("{id}")]
    public IActionResult GetDirectorById(int id)
    {
        var query = new GetDirectorByIdQuery(context,mapper);
        query.DirectorId = id;

        var validator = new GetDirectorByIdQueryValidator();
        validator.ValidateAndThrow(query);

        var movie = query.Handle();

        return Ok(movie);
    }

    [HttpPost]
    public IActionResult CreateDirector([FromBody] CreateDirectorModel newDirector)
    {
        var command = new CreateDirectorCommand(context,mapper);
        command.Model=newDirector;

        var validator = new CreateDirectorCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateDirector(int id, [FromBody] UpdateDirectorModel updatedDirector)
    {
        var command = new UpdateDirectorCommand(context,mapper);
        command.DirectorId = id;
        command.Model=updatedDirector;

        var validator = new UpdateDirectorCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteDirector(int id)
    {
        var command = new DeleteDirectorCommand(context);
        command.DirectorId = id;

        var validator = new DeleteDirectorCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok();
    }
}