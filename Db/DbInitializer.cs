using BadgeMeUp.Models;
using Microsoft.EntityFrameworkCore;

namespace BadgeMeUp.Db;

public static class DbInitializer
{
    public static void Initialize(this BadgeContext context)
    {
        // TODO: this should be Migrate(), but since prod is using this method it's not an easy cutover
        // See this: https://stackoverflow.com/questions/38238043/how-and-where-to-call-database-ensurecreated-and-database-migrate
        context.Database.EnsureCreated();

        if(context.Users.Any())
        {
            return;
        }

        var badgeTypes = new[]
        {
            new BadgeType
            {
                Name = "Skill"
            },
            new BadgeType
            {
                Name = "Customer Obsession"
            },
            new BadgeType
            {
                Name = "Fun"
            },
            new BadgeType
            {
                Name = "Event"
            },
            new BadgeType
            {
                Name = "Kudos"
            }
        };
        context.BadgeTypes.AddRange(badgeTypes);
        context.SaveChanges();

        var badges = new[]
        {
            new Badge
            {
                Name = "Azure Functions", Criteria = "Test Criteria", BadgeType = badgeTypes[0], Description = "Description for Azure Functions"
            },
            new Badge
            {
                Name = "Wall of Guitars", Criteria = "Must have a wall of guitars on the wall in view of your camera for Teams", BadgeType = badgeTypes[2], Description = "Has a wall of guitars as a real background"
            }
        };
        context.Badges.AddRange(badges);
        context.SaveChanges();

        var users = new[]
        {
            new User
            {
                PrincipalName = "jason@microsoft.com", PrincipalId = Guid.Parse("d4609816-ed54-4942-9fae-2b72c63b73ea")
            },
            new User
            {
                PrincipalName = "labrina@microsoft.com", PrincipalId = Guid.NewGuid()
            },
            new User
            {
                PrincipalName = "brandon@microsoft.com", PrincipalId = Guid.NewGuid()
            }
        };
        context.Users.AddRange(users);
        context.SaveChanges();

        var assignedBadges = new[]
        {
            new AssignedBadge(badges[0], users[0], users[0]), new AssignedBadge(badges[1], users[0], users[0]), new AssignedBadge(badges[0], users[0], users[1])
        };
        context.AssignedBadges?.AddRange(assignedBadges);
        context.SaveChanges();
    }
}