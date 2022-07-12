using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.MovieOperations.Queries.GetMovies;

public class GetMoviesQuery 
{
    private readonly IMovieStoreDbContext context;
    private readonly IMapper mapper;

    public GetMoviesQuery(IMovieStoreDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public List<GetMoviesViewModel> Handle()
    {
        var movies = context.Movies.Include(m=> m.CustomerMovies).Where(m => m.IsActive == true).OrderBy(m=>m.Id).ToList();

        var moviesViewModel = mapper.Map<List<GetMoviesViewModel>>(movies);

        return moviesViewModel;
    }
}

public class GetMoviesViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int Year { get; set; }
    public double Price { get; set; }

    public int GenreId { get; set; }    
    public int DirectorId { get; set; }

    // public ICollection<Actor> Actors { get; set; }
     public List<CustomerMovie> CustomerMovies { get; set; }
}