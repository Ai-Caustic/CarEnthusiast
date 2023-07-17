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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Message>().ToTable("Message");
                //.HasOne(a => a.Sender)
                //.WithMany(d => d.Messages)
                //.HasForeignKey(a => a.UserId);
            //modelBuilder.Entity<CarDetail>().ToTable("CarDetail");
            modelBuilder.Entity<Car>().ToTable("Car");
        }
    }
}
