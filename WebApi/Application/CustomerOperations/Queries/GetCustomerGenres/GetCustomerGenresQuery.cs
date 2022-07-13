using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.CustomerOperations.Queries.GetCustomerById;

public class GetCustomerGenresQuery 
{
    public int CustomerId { get; set; }
    private readonly IMovieStoreDbContext context;
    private readonly IMapper mapper;

    public GetCustomerGenresQuery(IMovieStoreDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public List<GetCustomerGenresViewModel> Handle()
    {
        var customer = context.Customers.Include(c=> c.Genres).SingleOrDefault(m => m.Id == CustomerId && m.IsActive == true);

        if(customer is null)
            throw new InvalidOperationException("CustomerId: " + CustomerId + " does not exist.");
        
        if(!customer.Genres.Any())
            throw new InvalidOperationException("CustomerId: " + CustomerId + " does not have any genre.");

        var customerGenres = mapper.Map<List<GetCustomerGenresViewModel>>(customer.Genres);

        return customerGenres;
    }
}

public class GetCustomerGenresViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
}