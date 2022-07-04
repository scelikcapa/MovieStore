using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.ActorOperations.Queries.GetActorById;

public class GetActorByIdQuery 
{
    public int ActorId { get; set; }
    private readonly IMovieStoreDbContext context;
    private readonly IMapper mapper;

    public GetActorByIdQuery(IMovieStoreDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public GetActorByIdViewModel Handle()
    {
        var actor = context.Actors.Include(a=> a.Movies).SingleOrDefault(m => m.Id == ActorId);

        if(actor is null)
            throw new InvalidOperationException("ActorId: "+ActorId+" does not exist");

        var actorViewModel = mapper.Map<GetActorByIdViewModel>(actor);

        return actorViewModel;
    }
}

public class GetActorByIdViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public ICollection<Movie>? Movies { get; set; }
}