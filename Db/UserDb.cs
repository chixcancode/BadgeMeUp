using BadgeMeUp.Models;
using Microsoft.EntityFrameworkCore;

namespace BadgeMeUp.Db;

public class UserDb
{
    private readonly BadgeContext _context;

    public UserDb(BadgeContext context)
    {
        _context = context;
    }

    public async Task AssignBadgeToUser(User fromUser, User toUser, Badge badge, string? comment)
    {
        var newAssignment = new AssignedBadge(badge, fromUser, toUser);
        if(comment != null)
        {
            newAssignment.AwardComment = comment;
        }

        _context.AssignedBadges.Add(newAssignment);
        await _context.SaveChangesAsync();
    }

    public async Task<List<User>> GetAllUsers()
    {
        return await _context.Users.OrderBy(x => x.PrincipalName).ToListAsync();
    }

    public async Task<User> GetOrCreateUser(Guid principalGuid, string principalName)
    {
        var user = await GetUser(principalGuid);

        if(user == null)
        {
            user = new User();
            user.PrincipalId = principalGuid;
            user.PrincipalName = principalName;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        return user;
    }

    public async Task<User?> GetUser(Guid principalGuid)
    {
        return await _context.Users.SingleOrDefaultAsync(x => x.PrincipalId == principalGuid);
    }

    public List<User> GetUsersWithoutBadge(int badgeId)
    {
        var q = (from u in _context.Users
            from ab in _context.AssignedBadges
                .Where(ab => u == ab.User && ab.Badge.Id == badgeId)
                .DefaultIfEmpty()
            select new
            {
                u, ab
            }).Where(x => x.ab == null).Select(x => x.u);

        return q.ToList();
    }

    public async Task<bool> HasBadge(User user, Badge badge)
    {
        var found = await _context.AssignedBadges.SingleOrDefaultAsync(x => x.User == user && x.Badge == badge);
        return found != null;
    }

    public async Task RemoveBadgeFromUser(User user, Badge badge)
    {
        var assignment = _context.AssignedBadges.SingleOrDefault(x => x.Badge == badge && x.User == user);

        if(assignment != null)
        {
            _context.Remove(assignment);
            await _context.SaveChangesAsync();
        }
    }
}