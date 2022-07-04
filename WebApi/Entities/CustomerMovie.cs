using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class CustomerMovie
    {   
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }     
        public double Price { get; set; }
        public DateTime OrderDate { get; set; }
        
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int MovieId { get; set; }        
        public Movie Movie { get; set; }

        
    }
}