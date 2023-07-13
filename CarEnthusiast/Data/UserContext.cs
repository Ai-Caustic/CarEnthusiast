using CarEnthusiast.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarEnthusiast.Data
{
    public class UserContext : IdentityDbContext<User, Role, int>
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        { }

        //public DbSet<User> Users { get; set; }
        public DbSet<CarDetail> CarDetails { get; set; }
        public DbSet<Car> Cars { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<CarDetail>().ToTable("CarDetail");
            modelBuilder.Entity<Car>().ToTable("Car");
        }
    }
}
