using BadgeMeUp.Models;

namespace BadgeMeUp
{
    public class DbInitializer
    {
        public static void Initialize(BadgeContext context)
        {
            context.Database.EnsureCreated();

            if(context.Users != null && context.Users.Any())
            {
                return; //db already populated
            }

            var badges = new Badge[]
{
                new Badge { Name = "Test Badge", Criteria = "Test Criteria",
                    BadgeType = BadgeType.Fun, Description = "Test Description" },
                new Badge { Name = "Test Badge 2", Criteria = "Test Criteria 2",
                    BadgeType = BadgeType.SoftSkills, Description = "Test Description 2" }
};
            context.Badges?.AddRange(badges);
            context.SaveChanges();


            var users = new User[]
            {
                new User("jayoung"),
                new User("lalovi")
            };
            users[0].AssignedBadges = new List<Badge>(badges);

            foreach(User u in users)
            {
                context.Users?.Add(u);
            }
            context.SaveChanges();
        }
    }
}
