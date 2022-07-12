using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.TestSetup;

public static class Directors
{
    public static void CreateDirectors(this MovieStoreDbContext context)
    {
        context.Directors.AddRange(
            new Director{
                Name = "Lana",
                Surname = "Wachowski"
            },
            new Director{
                Name = "Frank",
                Surname = "Darabont"
            }        
        );

        context.SaveChanges();
    }
}