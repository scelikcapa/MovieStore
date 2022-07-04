using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.CustomerMoviesOperations.Commands.CreateCustomerMovies;

public class CreateCustomerMoviesCommand 
{
    private readonly IMovieStoreDbContext context;
    private readonly IMapper mapper;
    public int CustomerId { get; set; }

    public CreateCustomerMoviesModel Model { get; set; }


    public CreateCustomerMoviesCommand(IMovieStoreDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public void Handle()
    {
        var customer = context.Customers.Include(c=> c.CustomerMovies).SingleOrDefault(c=> c.Id == CustomerId);

        if(customer is null)
            throw new InvalidOperationException("CustomerId: "+CustomerId+" does not exist.");

        var movieAdding = context.Movies.SingleOrDefault(m=> m.Id == Model.MovieId && m.IsActive == true);

        if(movieAdding is null)
            throw new InvalidOperationException("MovieId:" + Model.MovieId + " is not found."); 

        customer.CustomerMovies.Add(
            new CustomerMovie{
                Price = movieAdding.Price,
                OrderDate = DateTime.Now,
                Movie = movieAdding
        });
        
        context.SaveChanges();
    }
}

public class CreateCustomerMoviesModel
{  
    public int MovieId { get; set; }         
}