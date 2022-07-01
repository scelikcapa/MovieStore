using AutoMapper;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.MovieOperations.Commands.DeleteMovie;

public class DeleteMovieCommand 
{
    private readonly IMovieStoreDbContext context;
    private readonly IMapper mapper;
    public int MovieId { get; set; }
    

    public DeleteMovieCommand(IMovieStoreDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public void Handle()
    {
        var movieInDb = context.Movies.SingleOrDefault(m=>m.Id == MovieId);

        if(movieInDb is null)
            throw new InvalidOperationException("Movie Id: "+MovieId+" does not exists.");
        
        movieInDb.IsActive = false;
        
        context.SaveChanges();
    }
}
