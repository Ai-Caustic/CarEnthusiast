using System.ComponentModel.DataAnnotations;

namespace CarEnthusiast.Models
{
    public class LoginViewModel
    {
        [Required]
        public string email { get; set; }

        [Required]
        public string password { get; set; }
    }
}
