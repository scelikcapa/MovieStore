using AutoMapper;
using WebApi.DbOperations;
using WebApi.TokenOperations;
using WebApi.TokenOperations.Models;

namespace WebApi.Application.UserOperations.Commands.CreateToken;

public class CreateTokenCommand
{
    private readonly IMovieStoreDbContext context;
    private readonly IConfiguration configuration;
    public CreateTokenModel Model { get; set; }

    public CreateTokenCommand(IMovieStoreDbContext context, IConfiguration configuration)
    {
        this.context = context;
        this.configuration = configuration;
    }

    public Token Handle()
    {
        var customer = context.Customers.SingleOrDefault(c => c.Email == Model.Email && c.Password == Model.Password);

        if(customer is not null)
        {
            var handler = new TokenHandler(configuration);
            var token = handler.CreateAccessToken(customer);

            customer.RefreshToken = token.RefreshToken;
            customer.RefreshTokenExpireDate = token.Expiration.AddMinutes(5);
            context.SaveChanges();

            return token;           
        }
        else
            throw new InvalidOperationException("Customer Name or Password is wrong!");
    }
}

public class CreateTokenModel
{
    public string Email { get; set; }
    public string Password { get; set; }
}