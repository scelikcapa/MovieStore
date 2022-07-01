using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.MovieOperations.Commands.CreateMovie;
using WebApi.Application.MovieOperations.Commands.DeleteMovie;
using WebApi.Application.MovieOperations.Commands.UpdateMovie;
using WebApi.Application.MovieOperations.Queries.GetMovieById;
using WebApi.Application.MovieOperations.Queries.GetMovies;
using WebApi.DbOperations;

namespace WebApi.Controllers;

[ApiController]
[Route("[Controller]")]
public class MoviesController : ControllerBase
{
    private readonly IMovieStoreDbContext context;
    private readonly IMapper mapper;

    public MoviesController(IMovieStoreDbContext context,IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetMovies()
    {
        var query = new GetMoviesQuery(context,mapper);
        var movies = query.Handle();

        return Ok(movies);
    }

    [HttpGet("{id}")]
    public IActionResult GetMovieById(int id)
    {
        var query = new GetMovieByIdQuery(context,mapper);
        query.MovieId = id;

        var validator = new GetMovieByIdQueryValidator();
        validator.ValidateAndThrow(query);

        var movie = query.Handle();

        return Ok(movie);
    }

    [HttpPost]
    public IActionResult CreateMovie([FromBody] CreateMovieModel newMovie)
    {
        var command = new CreateMovieCommand(context,mapper);
        command.Model=newMovie;

        var validator = new CreateMovieCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateMovie(int id, [FromBody] UpdateMovieModel updatedMovie)
    {
        var command = new UpdateMovieCommand(context,mapper);
        command.MovieId = id;
        command.Model=updatedMovie;

        var validator = new UpdateMovieCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteMovie(int id)
    {
        var command = new DeleteMovieCommand(context,mapper);
        command.MovieId = id;

        var validator = new DeleteMovieCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok();
    }
}