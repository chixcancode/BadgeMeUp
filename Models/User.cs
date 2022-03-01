namespace BadgeMeUp.Models;

public class User
{
    public ICollection<AssignedBadge> AssignedBadges { get; set; }

    public int Id { get; set; }

    public Guid PrincipalId { get; set; }

    public string PrincipalName { get; set; }
}