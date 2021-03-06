using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.DirectorOperations.Commands.DeleteDirector;

public class DeleteDirectorCommand 
{
    private readonly IMovieStoreDbContext context;
    public int DirectorId { get; set; }
    

    public DeleteDirectorCommand(IMovieStoreDbContext context)
    {
        this.context = context;
    }

    public void Handle()
    {
        var directorInDb = context.Directors.Include(d=> d.Movies).SingleOrDefault(m=>m.Id == DirectorId);

        if(directorInDb is null)
            throw new InvalidOperationException("DirectorId: "+DirectorId+" does not exists.");
        
        if(directorInDb.Movies.Any())
            throw new InvalidOperationException("DirectorId: " + DirectorId + " has " +directorInDb.Movies.Count()+ " movies. Please delete them first.");
            
        context.Directors.Remove(directorInDb);
        
        context.SaveChanges();
    }
}
