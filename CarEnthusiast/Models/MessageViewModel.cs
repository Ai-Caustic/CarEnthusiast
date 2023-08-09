using System;

namespace CarEnthusiast.Models
{
    public class MessageViewModel
    {
        public List<Message> UserMessages { get; set; }

        public List<Message> OtherMessages { get; set; }

        public int GroupId { get; set;}

        public int Id { get; set;}   

        public string GroupName { get; set;}

    }
}