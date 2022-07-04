using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities;

public class Movie 
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime Year { get ; set; }
    public double Price { get; set; }

    public int GenreId { get; set; }
    public Genre Genre { get; set; }
    
    public int DirectorId { get; set; }
    public Director Director { get; set; }

    // Use FluentApi for direct relation. Without FluentApi this will be null
    //public ICollection<Customer> Customers { get; set; }

    public ICollection<Actor> Actors { get; set; }
    public List<CustomerMovie> CustomerMovies { get; set; }
    public bool IsActive { get; set; } = true; 
}