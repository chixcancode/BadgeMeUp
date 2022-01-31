namespace BadgeMeUp.Badge.API.Models
{
    public class User
    {
        public User(Guid id, string alias, string email, string firstName, string lastName)
        {
            Id = id;
            Alias = alias;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
        }

        public Guid Id { get; private set; }
        public string Alias { get; private set; }
        public string Email { get; private set; }   
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public ICollection<BadgeAward> Badges { get; private set; } = new List<BadgeAward>();
     }
}
