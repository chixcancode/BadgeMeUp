namespace BadgeMeUp.Badge.API.Models
{
    public class BadgeType
    {
        public BadgeType(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
    }

    
}
