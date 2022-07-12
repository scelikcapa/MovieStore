using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.TestSetup;

public static class Genres 
{
    public static void CreateGenres(this MovieStoreDbContext context)
    {
        context.Genres.AddRange(
        new Genre{
            Name = "Drama"
        },
        new Genre{
            Name = "Action"
        },
        new Genre{
            Name = "Comedy"
        });

        context.SaveChanges();
    }
}