using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.ActorOperations.Commands.CreateActor;
using WebApi.Application.ActorOperations.Commands.DeleteActor;
using WebApi.Application.ActorOperations.Commands.UpdateActor;
using WebApi.Application.ActorOperations.Queries.GetActorById;
using WebApi.Application.ActorOperations.Queries.GetActors;
using WebApi.DbOperations;

namespace WebApi.Controllers;

[ApiController]
[Route("[Controller]")]
public class ActorsController : ControllerBase
{
    private readonly IMapper mapper;
    private readonly IMovieStoreDbContext context;

    public ActorsController(IMovieStoreDbContext context, IMapper mapper)
    {
        this.mapper = mapper;
        this.context = context;
    }

     [HttpGet]
    public IActionResult GetActors()
    {
        var query = new GetActorsQuery(context,mapper);
        var movies = query.Handle();

        return Ok(movies);
    }

    [HttpGet("{id}")]
    public IActionResult GetActorById(int id)
    {
        var query = new GetActorByIdQuery(context,mapper);
        query.ActorId = id;

        var validator = new GetActorByIdQueryValidator();
        validator.ValidateAndThrow(query);

        var movie = query.Handle();

        return Ok(movie);
    }

    [HttpPost]
    public IActionResult CreateActor([FromBody] CreateActorModel newActor)
    {
        var command = new CreateActorCommand(context,mapper);
        command.Model=newActor;

        var validator = new CreateActorCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateActor(int id, [FromBody] UpdateActorModel updatedActor)
    {
        var command = new UpdateActorCommand(context,mapper);
        command.ActorId = id;
        command.Model=updatedActor;

        var validator = new UpdateActorCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteActor(int id)
    {
        var command = new DeleteActorCommand(context);
        command.ActorId = id;

        var validator = new DeleteActorCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok();
    }
}