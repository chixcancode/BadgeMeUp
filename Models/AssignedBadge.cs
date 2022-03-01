namespace BadgeMeUp.Models;

public class AssignedBadge
{
    public AssignedBadge() { }

    public AssignedBadge(Badge badge, User fromUser, User user)
    {
        Badge = badge;
        FromUser = fromUser;
        User = user;
    }

    public string AwardComment { get; set; } = string.Empty;

    public Badge? Badge { get; }

    public DateTime DateAssigned { get; set; } = DateTime.UtcNow;

    public User? FromUser { get; }

    public int Id { get; init; }

    public User? User { get; }
}