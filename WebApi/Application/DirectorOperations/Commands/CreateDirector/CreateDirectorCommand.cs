using AutoMapper;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.DirectorOperations.Commands.CreateDirector;

public class CreateDirectorCommand 
{
    private readonly IMovieStoreDbContext context;
    private readonly IMapper mapper;
    public CreateDirectorModel Model { get; set; }
    

    public CreateDirectorCommand(IMovieStoreDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public void Handle()
    {
        var directorInDb = context.Directors.SingleOrDefault(m=>m.Name.ToLower() == Model.Name.ToLower() && m.Surname.ToLower() == Model.Surname.ToLower());

        if(directorInDb is not null)
            throw new InvalidOperationException("DirectorNameSurname: " + Model.Name+" "+Model.Surname + " already exists.");

        var newDirector = mapper.Map<Director>(Model);

        context.Directors.Add(newDirector);
        context.SaveChanges();
    }
}

public class CreateDirectorModel 
{
        public string Name { get; set; }
        public string Surname { get; set; }
}