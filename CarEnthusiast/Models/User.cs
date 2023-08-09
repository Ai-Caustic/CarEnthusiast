using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarEnthusiast.Models
{
    public class User : IdentityUser
    {

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<Car> Cars { get; set; } = new List<Car>();

        public ICollection<UserGroup> UserGroups { get; set; } = new List<UserGroup>();

        public ICollection<Message> Messages { get; set; } = new List<Message>();

        
        //public virtual ICollection<Message> Messages { get; set; }

        //public int CarId { get; set; }
        //public ICollection<Car>? Cars { get; set; }
    }


}
