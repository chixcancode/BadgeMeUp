namespace BadgeMeUp.Models
{
    public class AssignedBadge
    {
        public int Id { get; set; }
        public Badge Badge { get; set; }
        public User FromUser { get; set; }
        public User User { get; set; }
        public DateTime DateAssigned { get; set; } = DateTime.UtcNow;
    }
}
