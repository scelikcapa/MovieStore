using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;

namespace WebApi.Application.CustomerMoviesOperations.Queries.GetCustomerMoviesById;

public class GetCustomerMoviesByIdQuery 
{
    private readonly IMovieStoreDbContext context;
    private readonly IMapper mapper;
    public int CustomerId { get; set; }

    public GetCustomerMoviesByIdQuery(IMovieStoreDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public List<GetCustomerMoviesByIdViewModel> Handle()
    {
        var customer = context.Customers.Include(c => c.CustomerMovies).SingleOrDefault(c => c.Id == CustomerId);

        if(customer is null)
            throw new InvalidOperationException("CustomerId: "+CustomerId+" does not exist.");

        if(!customer.CustomerMovies.Any())
            throw new InvalidOperationException("CustomerId: "+CustomerId+" does not have any movie.");

        var customerMovies = mapper.Map<List<GetCustomerMoviesByIdViewModel>>(customer.CustomerMovies.OrderBy(cm=> cm.Id));

        return customerMovies;
    }
}

public class GetCustomerMoviesByIdViewModel
{
    public int Id { get; set; }     
    public double Price { get; set; }
    public string OrderDate { get; set; }

    public int MovieId { get; set; }         
}