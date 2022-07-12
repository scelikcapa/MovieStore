using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.TestSetup;

public static class Actors 
{
    public static void CreateActors(this MovieStoreDbContext context)
    {
        context.Actors.AddRange(
            new Actor{
                Name = "Keanu",
                Surname = "Reeves",
            },
            new Actor{
                Name = "Laurence",
                Surname = "Fishburne"
            },
            new Actor{
                Name = "Tom",
                Surname = "Hanks"
            },
            new Actor{
                Name = "Michael Clarke",
                Surname = "Duncan"
            }   
        );

        context.SaveChanges();
    }
}