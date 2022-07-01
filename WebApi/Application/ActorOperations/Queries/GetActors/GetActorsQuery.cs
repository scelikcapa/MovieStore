using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.ActorOperations.Queries.GetActors;

public class GetActorsQuery 
{
    private readonly IMovieStoreDbContext context;
    private readonly IMapper mapper;

    public GetActorsQuery(IMovieStoreDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public List<GetActorsViewModel> Handle()
    {
        var actors = context.Actors.OrderBy(m=>m.Id).ToList();

        var actorsViewModel = mapper.Map<List<GetActorsViewModel>>(actors);

        return actorsViewModel;
    }
}

public class GetActorsViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public ICollection<Movie>? Movies { get; set; }
}