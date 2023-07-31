using Microsoft.AspNetCore.Identity;

namespace CarEnthusiast.Models
{
    public class Message
    {
        public int id {  get; set; }

        public string UserName { get; set; }

        public string Text { get; set; }

        public DateTime dateTime { get; set; } = DateTime.Now;

        public string UserId { get; set; }

        public virtual User Sender { get; set; }

        public int GroupId { get; set; }
    }
}
