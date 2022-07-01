using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities;

public class Customer 
{
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public ICollection<Movie> Movies { get; set; }
        public List<CustomerMovie> CustomerMovies { get; set; }
        
        public ICollection<Genre> Genres { get; set; }
}