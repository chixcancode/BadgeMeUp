namespace BadgeMeUp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Alias { get; set; }
        public ICollection<AssignedBadge> AssignedBadges { get; set; }
     }
}
