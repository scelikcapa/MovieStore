using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.CustomerOperations.Queries.GetCustomers;

public class GetCustomersQuery 
{
    private readonly IMovieStoreDbContext context;
    private readonly IMapper mapper;

    public GetCustomersQuery(IMovieStoreDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public List<GetCustomersViewModel> Handle()
    {
        var customers = context.Customers.OrderBy(m=>m.Id).ToList();

        var customersViewModel = mapper.Map<List<GetCustomersViewModel>>(customers);

        return customersViewModel;
    }
}

public class GetCustomersViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }

    public ICollection<Movie> Movies { get; set; }
    public ICollection<Genre> Genres { get; set; }
}