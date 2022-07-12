using WebApi.DbOperations;

namespace WebApi.Application.MovieOperations.Commands.DeleteMovie;

public class DeleteMovieCommand 
{
    private readonly IMovieStoreDbContext context;
    public int MovieId { get; set; }
    

    public DeleteMovieCommand(IMovieStoreDbContext context)
    {
        this.context = context;
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
