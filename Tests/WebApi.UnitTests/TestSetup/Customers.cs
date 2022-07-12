using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.TestSetup;

public static class Customers 
{
    public static void CreateCustomers(this MovieStoreDbContext context)
    {
        context.Customers.AddRange(
            new Customer{
                Name = "Samet",
                Surname = "Celikcapa",
                Genres = context.Genres.Where(g=>g.Id == 1 || g.Id == 2).ToList()
            },
            new Customer{
                Name = "Zeynep",
                Surname = "Çelikçapa",
                Genres = context.Genres.Where(g=>g.Id == 3).ToList()
            } 
        );
        
        context.SaveChanges();
    }
}