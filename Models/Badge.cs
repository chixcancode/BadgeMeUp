namespace BadgeMeUp.Models
{
    public enum BadgeType
    {
        Technical,
        SoftSkills,
        Fun
    }

    public class Badge
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Criteria { get; set; }
        public string Description { get; set; }
        //public User BadgeOwner { get; set; }
        public BadgeType BadgeType { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
