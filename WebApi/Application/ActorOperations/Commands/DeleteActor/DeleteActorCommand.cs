using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.ActorOperations.Commands.DeleteActor;

public class DeleteActorCommand 
{
    private readonly IMovieStoreDbContext context;
    public int ActorId { get; set; }
    

    public DeleteActorCommand(IMovieStoreDbContext context)
    {
        this.context = context;
    }

    public void Handle()
    {
        var actorInDb = context.Actors.Include(a=> a.Movies).SingleOrDefault(m=>m.Id == ActorId);

        if(actorInDb is null)
            throw new InvalidOperationException("ActorId: "+ActorId+" does not exists.");
        
        if(actorInDb.Movies.Any())
            throw new InvalidOperationException("ActorId: " + ActorId + " has " +actorInDb.Movies.Count()+ " movies. Please delete them first.");
            
        context.Actors.Remove(actorInDb);
        
        context.SaveChanges();
    }
}
