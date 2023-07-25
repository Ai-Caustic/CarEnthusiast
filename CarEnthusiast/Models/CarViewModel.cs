using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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


        public string? Showroom { get; set; }

        //Property to hold the Carimages
        public List<IFormFile> UploadedImages { get; set; }

     
    }
}
