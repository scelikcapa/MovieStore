using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.DirectorOperations.Queries.GetDirectorById;

public class GetDirectorByIdQuery 
{
    public int DirectorId { get; set; }
    private readonly IMovieStoreDbContext context;
    private readonly IMapper mapper;

    public GetDirectorByIdQuery(IMovieStoreDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public GetDirectorByIdViewModel Handle()
    {
        var director = context.Directors.SingleOrDefault(m => m.Id == DirectorId);

        if(director is null)
            throw new InvalidOperationException("DirectorId: "+DirectorId+" does not exist.");

        var directorViewModel = mapper.Map<GetDirectorByIdViewModel>(director);

        return directorViewModel;
    }
}

public class GetDirectorByIdViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public ICollection<Movie>? Movies { get; set; } 
}