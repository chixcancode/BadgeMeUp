using System.ComponentModel.DataAnnotations.Schema;

namespace BadgeMeUp.Models
{
    public class AssignedBadge
    {
        public int Id { get; set; }
        public Badge? Badge { get; private set; }
        public User? FromUser { get; private set; }
        public User? User { get; private set; }

        [ForeignKey("User")]
        public int UserId { get; }
        [ForeignKey("FromUser")]
        public int FromUserId { get; }
        public DateTime DateAssigned { get; set; } = DateTime.UtcNow;

        public string AwardComment { get; set; } = "";

        public AssignedBadge()
        {
        }

        public AssignedBadge(Badge badge, User fromUser, User user)
        {
            Badge = badge;
            FromUser = fromUser;
            User = user;
        }
    }
}
