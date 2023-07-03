﻿using CarEnthusiast.Models;
using Microsoft.EntityFrameworkCore;

namespace CarEnthusiast.Data
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        { }

        public DbSet<UserContext> Users { get; set; }
        public DbSet<CarDetail> CarDetails { get; set; }
        public DbSet<Car> Cars { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<CarDetail>().ToTable("CarDetail");
            modelBuilder.Entity<Car>().ToTable("Car");
        }
    }
}
