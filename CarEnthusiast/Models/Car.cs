using System;

namespace CarEnthusiast.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public DateTime Year { get; set; }
        public byte[]? Image { get; set; }
        public string? Showroom { get; set; }
    }
}