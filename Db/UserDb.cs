using BadgeMeUp.Models;
using Microsoft.EntityFrameworkCore;

namespace BadgeMeUp.Db
{
    public class UserDb
    {
        readonly BadgeContext _context;

        public UserDb(BadgeContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _context.Users.OrderBy(x => x.Alias).ToListAsync();
        }

        public List<User> GetUsersWithoutBadge(int badgeId)
        {
            var q = (from u in _context.Users
                     from ab in _context.AssignedBadges
                     .Where(ab => u == ab.User && ab.Badge.Id == badgeId)
                     .DefaultIfEmpty()
                     select new
                     {
                         u,
                         ab
                     }).Where(x => x.ab == null).Select(x => x.u);

            return q.ToList();
        }

        public async Task<User> GetUserById(int id)
        {
            return await _context.Users.SingleAsync(x => x.Id == id);
        }

        public async Task AssignBadgeToUser(User fromUser, User toUser, Badge badge)
        {
            var newAssignment = new AssignedBadge
            {
                Badge = badge,
                FromUser = fromUser,
                User = toUser,
                DateAssigned = DateTime.UtcNow
            };

            _context.AssignedBadges.Add(newAssignment);
            await _context.SaveChangesAsync();
        }
    }
}
