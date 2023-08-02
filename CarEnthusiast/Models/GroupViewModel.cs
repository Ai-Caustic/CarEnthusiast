using System.ComponentModel.DataAnnotations;

namespace CarEnthusiast.Models
{
    public class GroupViewModel
    {
        [Required]
        public string GroupName { get; set; }
    }

}
