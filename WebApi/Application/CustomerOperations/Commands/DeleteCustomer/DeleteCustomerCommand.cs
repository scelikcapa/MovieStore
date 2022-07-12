using AutoMapper;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.CustomerOperations.Commands.DeleteCustomer;

public class DeleteCustomerCommand 
{
    private readonly IMovieStoreDbContext context;
    public int CustomerId { get; set; }
    

    public DeleteCustomerCommand(IMovieStoreDbContext context)
    {
        this.context = context;
    }

    public void Handle()
    {
        var customerInDb = context.Customers.SingleOrDefault(m=>m.Id == CustomerId);

        if(customerInDb is null)
            throw new InvalidOperationException("CustomerId: "+CustomerId+" does not exists.");
        
        customerInDb.IsActive = false;
        
        context.SaveChanges();
    }
}
