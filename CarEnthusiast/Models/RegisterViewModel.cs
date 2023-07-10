using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarEnthusiast.Models
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(15, ErrorMessage ="Name length can't be more than 15.", MinimumLength =3)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        public string Email { get; set; }

        public DateTime CreatedAt
        {
            get { return DateTime.Now; }
        }

        [Required]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "Invalid Password")]
        public string Password { get; set; }

        
        [NotMapped] // Doesn't affect Database
        public string ConfirmPassword { get; set; }

    }
}
