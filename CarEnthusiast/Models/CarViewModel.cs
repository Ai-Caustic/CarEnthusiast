using System.ComponentModel.DataAnnotations;

namespace CarEnthusiast.Models
{
    public class CarViewModel
    {
        [Required]
        public string Make { get; set; }

        [Required]
        public required string Model { get; set; }

        [Required]
        public DateTime Year { get; set; }

        
        public byte[]? Image { get; set; }


        public string? Showroom { get; set; }

    }
}
