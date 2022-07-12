using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.TestSetup;

public static class CustomerMovies 
{
    public static void CreateCustomerMovies(this MovieStoreDbContext context)
    {
         context.CustomerMovies.AddRange(
                new CustomerMovie{
                    
                    CustomerId = 1,
                    MovieId = 1,
                    Price = 10,
                    OrderDate = DateTime.Now
                },
                new CustomerMovie{
                    CustomerId = 1,
                    MovieId = 2,
                    Price = 20,
                    OrderDate = DateTime.Now
                },
                new CustomerMovie{
                    CustomerId = 2,
                    MovieId = 2,
                    Price = 20,
                    OrderDate = DateTime.Now
                }
            );

        context.SaveChanges();
    }
}