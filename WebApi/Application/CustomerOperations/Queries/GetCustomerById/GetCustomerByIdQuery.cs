using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Application.CustomerMoviesOperations.Queries.GetCustomerMoviesById;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.CustomerOperations.Queries.GetCustomerById;

public class GetCustomerByIdQuery 
{
    public int CustomerId { get; set; }
    private readonly IMovieStoreDbContext context;
    private readonly IMapper mapper;

    public GetCustomerByIdQuery(IMovieStoreDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public GetCustomerByIdViewModel Handle()
    {
        var customer = context.Customers.Include(c=> c.CustomerMovies).SingleOrDefault(m => m.Id == CustomerId);

        if(customer is null)
            throw new InvalidOperationException("CustomerId: "+CustomerId+" does not exist.");

        var customerViewModel = mapper.Map<GetCustomerByIdViewModel>(customer);

        return customerViewModel;
    }
}

public class GetCustomerByIdViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public List<GetCustomerMoviesByIdViewModel> CustomerMovies { get; set; }
}