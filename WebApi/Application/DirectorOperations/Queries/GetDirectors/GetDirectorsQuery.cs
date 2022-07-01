using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.DirectorOperations.Queries.GetDirectors;

public class GetDirectorsQuery 
{
    private readonly IMovieStoreDbContext context;
    private readonly IMapper mapper;

    public GetDirectorsQuery(IMovieStoreDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public List<GetDirectorsViewModel> Handle()
    {
        var directors = context.Directors.OrderBy(m=>m.Id).ToList();

        var directorsViewModel = mapper.Map<List<GetDirectorsViewModel>>(directors);

        return directorsViewModel;
    }
}

public class GetDirectorsViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public ICollection<Movie>? Movies { get; set; }
}