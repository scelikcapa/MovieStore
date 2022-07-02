using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.CustomerMoviesOperations.Queries.GetCustomerMovies;

public class GetCustomerMoviesQuery 
{
    private readonly IMovieStoreDbContext context;
    private readonly IMapper mapper;
    public int CustomerId { get; set; }

    public GetCustomerMoviesQuery(IMovieStoreDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public List<GetCustomerMoviesViewModel> Handle()
    {
        var customer = context.Customers.Include(c => c.Movies).SingleOrDefault(c => c.Id == CustomerId);

        if(customer is null)
            throw new InvalidOperationException("CustomerId: "+CustomerId+" does not exist.");

        if(customer.Movies is null)
            throw new InvalidOperationException("CustomerId: "+CustomerId+" does not have any movie.");

        var customerMovies = mapper.Map<List<GetCustomerMoviesViewModel>>(customer.Movies);

        return customerMovies;
    }
}

public class GetCustomerMoviesViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime Year { get ; set; }
    public decimal Price { get; set; }

    public int GenreId { get; set; }
    public int DirectorId { get; set; }    
}