using AutoMapper;
using WebApi.DbOperations;
using WebApi.TokenOperations;
using WebApi.TokenOperations.Models;

namespace WebApi.Application.UserOperations.Commands.RefreshToken;

public class RefreshTokenCommand
{
    private readonly IMovieStoreDbContext context;
    private readonly IConfiguration configuration;
    public string RefreshToken { get; set; }

    public RefreshTokenCommand(IMovieStoreDbContext context, IConfiguration configuration)
    {
        this.context = context;
        this.configuration = configuration;
    }

    public Token Handle()
    {
        var customer = context.Customers.SingleOrDefault(c=> c.RefreshToken == RefreshToken && c.RefreshTokenExpireDate > DateTime.Now);

        if( customer is not null)
        {
            var handler = new TokenHandler(configuration);
            var token = handler.CreateAccessToken(customer);

            customer.RefreshToken = token.RefreshToken;
            customer.RefreshTokenExpireDate = token.Expiration.AddMinutes(5);
            context.SaveChanges();

            return token;
        }
        else    
            throw new InvalidOperationException("Valid Refresh Token is not found!");
    }
}
