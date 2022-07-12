using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.TestSetup;

public static class Movies 
{
    public static void CreateMovies(this MovieStoreDbContext context)
    {
        context.Movies.AddRange(
                new Movie{
                    Title = "Matrix",
                    Year = new DateTime(2000,01,01),
                    Price = 10,
                    GenreId = 2,
                    DirectorId = 1,
                    Actors = context.Actors.Where(a=>a.Id == 1 || a.Id == 2).ToList()
                },
                new Movie{
                    Title = "Green Mile",
                    Year = new DateTime(2005,01,01),
                    Price = 20,
                    GenreId = 1,
                    DirectorId = 2,
                    Actors = context.Actors.Where(a=>a.Id == 3 || a.Id == 4).ToList()
                }
            );

        context.SaveChanges();
    }
}