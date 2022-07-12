using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.DbOperations;

public class MovieStoreDbContext : DbContext, IMovieStoreDbContext
{
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Director> Directors { get; set; }
    public DbSet<Actor> Actors { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<CustomerMovie> CustomerMovies { get; set; }

    public MovieStoreDbContext(DbContextOptions<MovieStoreDbContext> options) : base (options)
    {
        
    }

    public override int SaveChanges()
    {
        return base.SaveChanges();
    }
 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        // IF YOU NEED TO DIRECT RELATION BETWEEN MANY-TO-MANY TABLES, YOU HAVE TO USE FLUENTAPI. INDIRECT RELATION CAN BE SETUP WITHOUT FLUENTAPI
        //   modelBuilder.Entity<Customer>()
        //     .HasMany(p => p.Movies)
        //     .WithMany(p => p.Customers)
        //     .UsingEntity<CustomerMovie>(
        //         j => j
        //             .HasOne(pt => pt.Movie)
        //             .WithMany(t => t.CustomerMovies)
        //             .HasForeignKey(pt => pt.MovieId),
        //         j => j
        //             .HasOne(pt => pt.Customer)
        //             .WithMany(p => p.CustomerMovies)
        //             .HasForeignKey(pt => pt.CustomerId),
        //         j =>
        //         {
        //             j.HasKey(pt=> pt.Id);
        //             j.Property(pt => pt.OrderDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
        //         });
    }

}