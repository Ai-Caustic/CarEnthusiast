using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarEnthusiast.Models
{
    public class User : IdentityUser<int>
    {
       // public int Id { get; set; }
       // public required string Name { get; set; }
       // public required string Email { get; set; }
        public required string Password { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<Car>? Cars { get; set; }

        //public int CarId { get; set; }
        //public ICollection<Car>? Cars { get; set; }
    }

    public class Role : IdentityRole<int>
    { }
}
