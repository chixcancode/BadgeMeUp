namespace BadgeMeUp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Alias { get; set; }
        public ICollection<Badge> AssignedBadges { get; set; }
                
        public User(string? alias)
        {
            this.Alias = alias ?? "";
            this.AssignedBadges = new List<Badge>();
        }

     }
}
