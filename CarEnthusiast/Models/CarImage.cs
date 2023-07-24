using System;
namespace CarEnthusiast.Models
{
    public class CarImage
    {
        public int Id { get; set; }
        public byte[] ImageData { get; set; }
        public string FileName { get; set; }


        // Navigation
        public int CarId { get; set; }
        public Car Car { get; set; }
    }
}
