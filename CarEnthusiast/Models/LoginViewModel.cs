using System.ComponentModel.DataAnnotations;

namespace CarEnthusiast.Models
{
    public class LoginViewModel
    {
        [Required]
        [StringLength(15, ErrorMessage = "Name length can't be more than 15.", MinimumLength = 3)]
        public string email { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "Invalid Password")]
        public string password { get; set; }
    }
}
