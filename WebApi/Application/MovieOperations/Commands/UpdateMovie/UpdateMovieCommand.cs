using AutoMapper;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.MovieOperations.Commands.UpdateMovie;

public class UpdateMovieCommand 
{
    private readonly IMovieStoreDbContext context;
    private readonly IMapper mapper;
    
    public int MovieId { get; set; }
    public UpdateMovieModel Model { get; set; }
    

    public UpdateMovieCommand(IMovieStoreDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public void Handle()
    {
        var movieInDb = context.Movies.SingleOrDefault(m=>m.Id == MovieId);

        if(movieInDb is null)
            throw new InvalidOperationException("MovieId: "+MovieId+" does not exist.");
        
        bool isSameTitleExists = context.Movies.Where(m=>
                                                    m.Title.ToLower() == (Model.Title != null ? Model.Title.ToLower() : movieInDb.Title.ToLower()) && 
                                                    m.Id != MovieId)
                                                .Any();
        
        if(isSameTitleExists)
            throw new InvalidOperationException("MovieTitle: "+ Model.Title +" already exists, choose another name.");

        mapper.Map<UpdateMovieModel, Movie>(Model, movieInDb);

        context.SaveChanges();
    }
}

public class UpdateMovieModel 
{
    public string? Title { get; set; }
    public int? Year { get; set; }
    public double? Price { get; set; }

    public int? GenreId { get; set; }    
    public int? DirectorId { get; set; }
    
    public bool? IsActive { get; set; }
    
}