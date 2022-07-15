using AutoMapper;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.CustomerOperations.Commands.UpdateCustomer;

public class UpdateCustomerCommand 
{
    private readonly IMovieStoreDbContext context;
    private readonly IMapper mapper;
    
    public int CustomerId { get; set; }
    public UpdateCustomerModel Model { get; set; }
    

    public UpdateCustomerCommand(IMovieStoreDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public void Handle()
    {
        var customerInDb = context.Customers.SingleOrDefault(m=>m.Id == CustomerId);

        if(customerInDb is null)
            throw new InvalidOperationException("CustomerId: "+CustomerId+" does not exist.");
        
        bool isSameNameExists = context.Customers.Where(m =>
                                                    m.Name.ToLower() == (Model.Name.ToLower() ?? customerInDb.Name.ToLower()) && 
                                                    m.Surname.ToLower() == (Model.Surname.ToLower() ?? customerInDb.Surname.ToLower()) && 
                                                    m.Id != CustomerId).Any();
        
        if(isSameNameExists)
            throw new InvalidOperationException("CustomerNameSurname: "+ Model.Name +" "+Model.Surname+" already exists, choose another name.");

        mapper.Map<UpdateCustomerModel, Customer>(Model, customerInDb);

        context.SaveChanges();
    }
}

public class UpdateCustomerModel 
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
}