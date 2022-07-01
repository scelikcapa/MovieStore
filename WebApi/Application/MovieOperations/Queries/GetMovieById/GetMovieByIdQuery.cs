using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.MovieOperations.Queries.GetMovieById;

public class GetMovieByIdQuery 
{
    public int MovieId { get; set; }
    private readonly IMovieStoreDbContext context;
    private readonly IMapper mapper;

    public GetMovieByIdQuery(IMovieStoreDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public GetMovieByIdViewModel Handle()
    {
        var movie = context.Movies.SingleOrDefault(m => m.Id == MovieId && m.IsActive == true);

        if(movie is null)
            throw new InvalidOperationException("MovieId: "+MovieId+" does not exist");

        var movieViewModel = mapper.Map<GetMovieByIdViewModel>(movie);

        return movieViewModel;
    }
}

public class GetMovieByIdViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Year { get; set; }
    public decimal Price { get; set; }

    public int GenreId { get; set; }
    public int DirectorId { get; set; }

    // public ICollection<Actor> Actors { get; set; }
    // public ICollection<Customer> Customers { get; set; }
}