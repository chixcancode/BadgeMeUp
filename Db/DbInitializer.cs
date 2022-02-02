using BadgeMeUp.Models;

namespace BadgeMeUp.Db
{
    public class DbInitializer
    {
        public static void Initialize(BadgeContext context)
        {
            context.Database.EnsureCreated();

            if (context.Users != null && context.Users.Any())
            {
                return; //db already populated
            }

            var badgeTypes = new BadgeType[]
            {
                new BadgeType { Name = "Skill" },
                new BadgeType { Name = "Customer Obsession" },
                new BadgeType { Name = "Fun" }
            };
            context.BadgeTypes?.AddRange(badgeTypes);
            context.SaveChanges();


            var badges = new Badge[]
            {
                new Badge { Name = "Azure Functions", Criteria = "Test Criteria",
                    BadgeType = badgeTypes[0], Description = "Description for Azure Functions" },
                new Badge { Name = "Wall of Guitars", Criteria = "Must have a wall of guitars on the wall in view of your camera for Teams",
                    BadgeType = badgeTypes[2], Description = "Has a wall of guitars as a real background" }
            };
            context.Badges?.AddRange(badges);
            context.SaveChanges();


            var users = new User[]
            {
                new User { PrincipalName = "jason@microsoft.com", PrincipalId = Guid.Parse("d4609816-ed54-4942-9fae-2b72c63b73ea") },
                new User { PrincipalName = "labrina@microsoft.com", PrincipalId = Guid.NewGuid() },
                new User { PrincipalName = "brandon@microsoft.com", PrincipalId = Guid.NewGuid() },
            };
            context.Users?.AddRange(users);
            context.SaveChanges();


            var assignedBadges = new AssignedBadge[]
            {
                new AssignedBadge(badges[0], users[0], users[0]),
                new AssignedBadge(badges[1], users[0], users[0]),
                new AssignedBadge(badges[0], users[0], users[1])
            };
            context.AssignedBadges?.AddRange(assignedBadges);
            context.SaveChanges();
        }
    }
}
