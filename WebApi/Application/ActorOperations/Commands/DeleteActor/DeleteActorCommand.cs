using AutoMapper;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.ActorOperations.Commands.DeleteActor;

public class DeleteActorCommand 
{
    private readonly IMovieStoreDbContext context;
    private readonly IMapper mapper;
    public int ActorId { get; set; }
    

    public DeleteActorCommand(IMovieStoreDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public void Handle()
    {
        var actorInDb = context.Actors.SingleOrDefault(m=>m.Id == ActorId);

        if(actorInDb is null)
            throw new InvalidOperationException("ActorId: "+ActorId+" does not exists.");
        
        context.Actors.Remove(actorInDb);
        
        context.SaveChanges();
    }
}