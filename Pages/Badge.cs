namespace BadgeMeUp.Pages
{
    public enum BadgeType
    {
        Technical,
        SoftSkills,
        Fun
    }

    public class Badge
    {
        public string Name { get; set; }
        public string Criteria { get; set; }
        public string Description { get; set; }
        public string BadgeOwnerAlias { get; set; }
        public BadgeType BadgeType { get; set; }

        public Badge(string name, string criteria, string description, string badgeOwnerAlias, BadgeType badgeType)
        {
            Name = name;
            Criteria = criteria;
            Description = description;
            BadgeOwnerAlias = badgeOwnerAlias;
            BadgeType = badgeType;
        }

        public Badge()
        {

        }
    }
}
