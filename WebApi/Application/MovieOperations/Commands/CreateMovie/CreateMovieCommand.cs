using AutoMapper;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.MovieOperations.Commands.CreateMovie;

public class CreateMovieCommand 
{
    private readonly IMovieStoreDbContext context;
    private readonly IMapper mapper;
    public CreateMovieModel Model { get; set; }
    

    public CreateMovieCommand(IMovieStoreDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public void Handle()
    {
        var movieInDb = context.Movies.SingleOrDefault(m=>m.Name.ToLower() == Model.Name.ToLower());

        if(movieInDb is not null)
            throw new InvalidOperationException("Movie Name: " + Model.Name + "already exists.");

        var newMovie = mapper.Map<Movie>(Model);

        context.Movies.Add(newMovie);
        context.SaveChanges();
    }
}

public class CreateMovieModel 
{
    public string Name { get; set; }
    public int Year { get; set; }
    public double Price { get; set; }

    public int GenreId { get; set; }    
    public int DirectorId { get; set; }
    
}