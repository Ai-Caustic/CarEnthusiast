using CarEnthusiast.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarEnthusiast.Data
{
    public class UserContext : IdentityDbContext<User>
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        { }

        public DbSet<User> User { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<CarImage> CarImages { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }

    }
}
