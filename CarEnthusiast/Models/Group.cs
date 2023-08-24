namespace CarEnthusiast.Models
{
    public class Group
    {
        public int Id { get; set; }

        public string? GroupName { get; set; }

        public string? GroupDescription { get; set; }   

        //Message collection
        public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}
