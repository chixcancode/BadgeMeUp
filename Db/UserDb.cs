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
