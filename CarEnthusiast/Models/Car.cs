using System;
using Microsoft.AspNetCore.Identity;

namespace CarEnthusiast.Models
{
    public class Car
    {
        public int Id { get; set; }
        public required string Make { get; set; }
        public required string Model { get; set; }
        public DateTime Year { get; set; }
        public byte[]? Image { get; set; }
        public string? Showroom { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
        //public List<CarDetail> CarDetails { get; set; }
    }
}