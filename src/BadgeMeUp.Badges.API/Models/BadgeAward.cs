namespace BadgeMeUp.Badge.API.Models
{
    public class BadgeAward
    {
        public BadgeAward(Guid badgeId, Guid fromUserId, Guid awardeeId,
            DateTime awardDate, string? comments)
        { 
            BadgeId = badgeId;
            FromUserId = fromUserId;
            AwardeeId = awardeeId;
            AwardDate = awardDate;
            Comments = comments;
        }
        public Guid BadgeId { get; private set; }
   
        public Badge? Badge { get; private set; }

        public Guid FromUserId { get; private set; }
        public User? FromUser { get; private set; }
        public Guid AwardeeId { get; private set; }
        public User? Awardee { get; private set; }
        public DateTime AwardDate { get; private set; } = DateTime.UtcNow;
        public string? Comments { get; private set; }
        
    }
}
