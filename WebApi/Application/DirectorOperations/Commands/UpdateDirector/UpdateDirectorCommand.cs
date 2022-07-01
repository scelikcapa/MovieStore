using AutoMapper;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.DirectorOperations.Commands.UpdateDirector;

public class UpdateDirectorCommand 
{
    private readonly IMovieStoreDbContext context;
    private readonly IMapper mapper;
    
    public int DirectorId { get; set; }
    public UpdateDirectorModel Model { get; set; }
    

    public UpdateDirectorCommand(IMovieStoreDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public void Handle()
    {
        var directorInDb = context.Directors.SingleOrDefault(m=>m.Id == DirectorId);

        if(directorInDb is null)
            throw new InvalidOperationException("DirectorId: "+DirectorId+" does not exist.");
        
        bool isSameNameExists = context.Directors.Where(m=>m.Name.ToLower() == Model.Name.ToLower() && m.Surname.ToLower() == Model.Surname.ToLower() && m.Id != DirectorId).Any();
        
        if(isSameNameExists)
            throw new InvalidOperationException("DirectorNameSurname: "+ Model.Name+" "+Model.Surname+" already exists, choose another name.");

        mapper.Map<UpdateDirectorModel, Director>(Model, directorInDb);

        context.SaveChanges();
    }
}

public class UpdateDirectorModel 
{
    public string Name { get; set; }
    public string Surname { get; set; }    
}