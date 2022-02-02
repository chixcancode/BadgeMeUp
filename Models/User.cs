namespace BadgeMeUp.Models
{
    public class User
    {
        public int Id { get; set; }
        public ICollection<AssignedBadge> AssignedBadges { get; set; }
        public string PrincipalName { get; set; }
        public Guid PrincipalId { get; set; }
     }
}
