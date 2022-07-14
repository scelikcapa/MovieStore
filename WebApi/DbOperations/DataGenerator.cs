using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.DbOperations;

public class DataGenerator 
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new MovieStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<MovieStoreDbContext>>()))
        {
            if(context.Genres.Any() || context.Directors.Any() || context.Movies.Any())
                return;

            context.Genres.AddRange(
                new Genre{
                    Name = "Drama"
                },
                new Genre{
                    Name = "Action"
                },
                new Genre{
                    Name = "Comedy"
                }
            );

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

            context.Customers.AddRange(
                new Customer{
                    Name = "Samet",
                    Surname = "Celikcapa",
                    Email = "samet@mail.com",
                    Password = "password",
                    Genres = context.Genres.Where(g=>g.Id == 1 || g.Id == 2).ToList()
                },
                new Customer{
                    Name = "Zeynep",
                    Surname = "Çelikçapa",
                    Email = "zeynep@mail.com",
                    Password = "password",
                    Genres = context.Genres.Where(g=>g.Id == 3).ToList()
                } 
            );

            context.SaveChanges();

            context.Movies.AddRange(
                new Movie{
                    Title = "Matrix",
                    Year = new DateTime(2000,01,01),
                    Price = 10,
                    GenreId = 2,
                    DirectorId = 1,
                    Actors = context.Actors.Where(a=>a.Id == 1 || a.Id == 2).ToList()
                    // Customers = context.Customers.Where(a=>a.Id==1).ToList()
                },
                new Movie{
                    Title = "Green Mile",
                    Year = new DateTime(2005,01,01),
                    Price = 20,
                    GenreId = 1,
                    DirectorId = 2,
                    Actors = context.Actors.Where(a=>a.Id == 3 || a.Id == 4).ToList()
                    // Customers = context.Customers.Where(a=>a.Id==1 || a.Id==2).ToList()
                }
            );

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
}