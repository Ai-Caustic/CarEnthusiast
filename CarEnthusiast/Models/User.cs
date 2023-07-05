using System;

namespace CarEnthusiast.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<Car> Cars { get; set; }

        //public int CarId { get; set; }
        //public ICollection<Car>? Cars { get; set; }
    }
}
