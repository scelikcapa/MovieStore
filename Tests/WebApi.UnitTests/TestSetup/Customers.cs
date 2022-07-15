using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.TestSetup;

public static class Customers 
{
    public static void CreateCustomers(this MovieStoreDbContext context)
    {
        var genres = context.Genres.ToList();

        context.Customers.AddRange(
            new Customer{
                Name = "Samet",
                Surname = "Celikcapa",
                Email = "samet@mail.com",
                Password = "password",
                Genres = genres.Where(g=> g.Id == 1 || g.Id == 2).ToList()
            },
            new Customer{
                Name = "Zeynep",
                Surname = "Celikçapa",
                Email = "zeynep@mail.com",
                Password = "password",
                Genres = genres.Where(g=> g.Id == 3).ToList()
            } 
        );

        context.SaveChanges();
    }
}