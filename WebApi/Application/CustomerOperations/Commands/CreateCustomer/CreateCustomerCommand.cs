using AutoMapper;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.CustomerOperations.Commands.CreateCustomer;

public class CreateCustomerCommand 
{
    private readonly IMovieStoreDbContext context;
    private readonly IMapper mapper;
    public CreateCustomerModel Model { get; set; }
    

    public CreateCustomerCommand(IMovieStoreDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public void Handle()
    {
        var customerInDb = context.Customers.SingleOrDefault(m=>m.Email.ToLower() == Model.Email.ToLower());
        
        if(customerInDb is not null)
            throw new InvalidOperationException("CustomerEmail: " + Model.Email +" already exists.");
        
        customerInDb = context.Customers.SingleOrDefault(m=>m.Name.ToLower() == Model.Name.ToLower() && m.Surname.ToLower() == Model.Surname.ToLower());

        if(customerInDb is not null)
            throw new InvalidOperationException("CustomerNameSurname: " + Model.Name +" "+Model.Surname + " already exists.");

        var newCustomer = mapper.Map<Customer>(Model);

        context.Customers.Add(newCustomer);
        context.SaveChanges();
    }
}

public class CreateCustomerModel 
{
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
}