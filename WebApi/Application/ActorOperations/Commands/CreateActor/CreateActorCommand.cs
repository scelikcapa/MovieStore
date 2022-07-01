using AutoMapper;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.ActorOperations.Commands.CreateActor;

public class CreateActorCommand 
{
    private readonly IMovieStoreDbContext context;
    private readonly IMapper mapper;
    public CreateActorModel Model { get; set; }
    

    public CreateActorCommand(IMovieStoreDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public void Handle()
    {
        var actorInDb = context.Actors.SingleOrDefault(m=>m.Name.ToLower() == Model.Name.ToLower() && m.Surname.ToLower() == Model.Surname.ToLower());

        if(actorInDb is not null)
            throw new InvalidOperationException("ActorNameSurname: " + Model.Name+" "+Model.Surname + " already exists.");

        var newActor = mapper.Map<Actor>(Model);

        context.Actors.Add(newActor);
        context.SaveChanges();
    }
}

public class CreateActorModel 
{
        public string Name { get; set; }
        public string Surname { get; set; }
}