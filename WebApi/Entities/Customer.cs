using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities;

public class Customer 
{
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpireDate { get; set; }

        // Use FluentApi for direct relation. Without FluentApi this will be null
        // public ICollection<Movie> Movies { get; set; }

        public ICollection<Genre> Genres { get; set; }
        public List<CustomerMovie> CustomerMovies { get; set; }
        public bool IsActive { get; set; } = true;
}