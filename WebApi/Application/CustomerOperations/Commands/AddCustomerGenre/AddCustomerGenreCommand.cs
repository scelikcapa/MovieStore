using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.CustomerOperations.Commands.AddCustomerGenre;

public class AddCustomerGenreCommand 
{
    private readonly IMovieStoreDbContext context;
    private readonly IMapper mapper;
    public int CustomerId { get; set; }
    public AddCustomerGenreModel Model { get; set; }

    public AddCustomerGenreCommand(IMovieStoreDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public void Handle()
    {
        var customer = context.Customers.Include(c => c.Genres).SingleOrDefault(c=>c.Id == CustomerId);

        if(customer is null)
            throw new InvalidOperationException("CustomerId: " + CustomerId + " does not exist.");

        var genreInDb = context.Genres.SingleOrDefault(g=> g.Id == Model.GenreId);

        if (genreInDb is null)
            throw new InvalidOperationException("GenreId: " + Model.GenreId + " not found.");

        if(customer.Genres.Contains(genreInDb))
            throw new InvalidOperationException("Same GenreId:" + Model.GenreId + " already added to CustomerId: "+ CustomerId);

        customer.Genres.Add(genreInDb);            

        context.SaveChanges();
    }

}

public class AddCustomerGenreModel
{
    public int GenreId { get; set; }
}