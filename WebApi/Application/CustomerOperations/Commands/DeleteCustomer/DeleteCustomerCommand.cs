using AutoMapper;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.CustomerOperations.Commands.DeleteCustomer;

public class DeleteCustomerCommand 
{
    private readonly IMovieStoreDbContext context;
    private readonly IMapper mapper;
    public int CustomerId { get; set; }
    

    public DeleteCustomerCommand(IMovieStoreDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public void Handle()
    {
        var customerInDb = context.Customers.SingleOrDefault(m=>m.Id == CustomerId);

        if(customerInDb is null)
            throw new InvalidOperationException("CustomerId: "+CustomerId+" does not exists.");
        
        context.Customers.Remove(customerInDb);
        
        context.SaveChanges();
    }
}
