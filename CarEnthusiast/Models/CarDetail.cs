namespace CarEnthusiast.Models
{
    public enum Type
    {
        Suv,
        Sports,
        Van,
        Truck,
        Saloon,
        Luxury
    }

    public class CarDetail
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CarId { get; set; }
        public DateTime DateAdded { get; set; }
        public Type? Type { get; set; }

        public User User { get; set; }
        public Car Car { get; set; }
    }
}
