using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.UserOperations.Commands.CreateToken;
using WebApi.Application.UserOperations.Commands.RefreshToken;
using WebApi.DbOperations;
using WebApi.TokenOperations.Models;

namespace WebApi.Controllers;

[ApiController]
[Route("[Controller]")]
public class UsersController : ControllerBase 
{
    private readonly IMovieStoreDbContext context;
    private readonly IMapper mapper;
    private readonly IConfiguration configuration;

    public UsersController(IMovieStoreDbContext context, IMapper mapper, IConfiguration configuration)
    {
        this.context = context;
        this.mapper = mapper;
        this.configuration = configuration;
    }

    [HttpPost("connect/token")]
    public ActionResult<Token> CreateToken([FromBody] CreateTokenModel login)
    {
        var command = new CreateTokenCommand(context, configuration);
        command.Model = login;
        var token = command.Handle();

        return Ok(token);
    }

    [HttpGet("connect/refreshToken")]
    public ActionResult<Token> RefreshToken([FromQuery] string refreshToken)
    {
        var command = new RefreshTokenCommand(context, configuration);
        command.RefreshToken = refreshToken;
        var token = command.Handle();

        return Ok(token);
    }


}