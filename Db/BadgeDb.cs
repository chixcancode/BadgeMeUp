using BadgeMeUp.Models;
using Microsoft.EntityFrameworkCore;

namespace BadgeMeUp.Db
{
    public class BadgeDb
    {
        private readonly BadgeContext _db;

        public BadgeDb(BadgeContext dbContext)
        {
            _db = dbContext;
        }

        public async Task<Badge?> GetBadge(int badgeId)
        {
            return await _db.Badges.Include(x => x.BadgeType).SingleOrDefaultAsync(m => m.Id == badgeId);
        }

        public async Task<List<AssignedBadge>> GetAssignedBadges(int userId)
        {
            return await _db.AssignedBadges.Include(x => x.Badge).Where(x => x.User.Id == userId).ToListAsync();
        }

        public async Task SaveBadge(Badge badge, AssignedBadge initialAssignment)
        {
            _db.Badges.Add(badge);
            _db.AssignedBadges.Add(initialAssignment);
            await _db.SaveChangesAsync();
        }

        public async Task<List<BadgeType>> GetAllBadgeTypes()
        {
            return await _db.BadgeTypes.ToListAsync();
        }

        public async Task<BadgeType?> GetBadgeType(int badgeId)
        {
            return await _db.BadgeTypes.SingleOrDefaultAsync(x => x.Id == badgeId);
        }

        public async Task UpdateBadge(Badge badge)
        {
            _db.Attach(badge).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public void DeleteBadge(Badge badge)
        {
            var assignmentsToDelete = _db.AssignedBadges.Where(x => x.Badge == badge);
            foreach(var assignment in assignmentsToDelete)
            {
                _db.Remove(assignment);
            }
            _db.Remove(badge);
            _db.SaveChanges();
        }
    }
}
