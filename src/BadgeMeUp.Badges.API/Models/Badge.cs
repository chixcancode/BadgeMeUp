namespace BadgeMeUp.Badge.API.Models
{
    public class Badge
    {

        public Badge(Guid id, string name, string description, string criteria, 
            Guid badgeCreatorId, Guid badgeOwnerId, int badgeTypeId)
        {
            Id = id;
            Name = name;
            Description = description;
            Criteria = criteria;
            BadgeCreatorId = badgeCreatorId;
            BadgeOwnerId = badgeOwnerId;
            BadgeTypeId = badgeTypeId;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        //Should this be a list instead?
        public string Criteria { get; private set; }
        public string Description { get; private set; }

        public Guid BadgeCreatorId { get; private set; }

        public User? BadgeCreator { get; private set; }

        public Guid BadgeOwnerId { get; private set; }

        public User? BadgeOwner { get; private set; }

        public int BadgeTypeId { get; private set; }
        public BadgeType? BadgeType { get; private set; }

    }
}
