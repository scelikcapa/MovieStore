using AutoMapper;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.ActorOperations.Commands.UpdateActor;

public class UpdateActorCommand 
{
    private readonly IMovieStoreDbContext context;
    private readonly IMapper mapper;
    
    public int ActorId { get; set; }
    public UpdateActorModel Model { get; set; }
    

    public UpdateActorCommand(IMovieStoreDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public void Handle()
    {
        var actorInDb = context.Actors.SingleOrDefault(m=>m.Id == ActorId);

        if(actorInDb is null)
            throw new InvalidOperationException("ActorId: "+ActorId+" does not exist.");
        
        bool isSameNameExists = context.Actors.Where(m=>
                                                m.Name.ToLower() == (Model.Name == null ? actorInDb.Name.ToLower() : Model.Name.ToLower()) && 
                                                    m.Surname.ToLower() == (Model.Surname == null ? actorInDb.Surname.ToLower() : Model.Surname.ToLower()) && 
                                                m.Id != ActorId)
                                              .Any();
        
        if(isSameNameExists)
            throw new InvalidOperationException("ActorNameSurname: "+ Model.Name+" "+Model.Surname+" already exists, choose another name.");

        mapper.Map<UpdateActorModel, Actor>(Model, actorInDb);

        context.SaveChanges();
    }
}

public class UpdateActorModel 
{
    public string? Name { get; set; }
    public string? Surname { get; set; }    
}