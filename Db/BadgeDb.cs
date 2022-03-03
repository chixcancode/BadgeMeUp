﻿using BadgeMeUp.Models;
using Microsoft.EntityFrameworkCore;

namespace BadgeMeUp.Db;

public class BadgeDb
{
    private readonly BadgeContext _db;

    public BadgeDb(BadgeContext dbContext)
    {
        _db = dbContext;
    }

    public async Task DeleteAssignedBadge(int badgeId)
    {
        var assignment = await GetAssignedBadge(badgeId);
        if(assignment != null)
        {
            _db.Remove(assignment);
            await _db.SaveChangesAsync();
        }
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

    public async Task<List<Badge>> GetAllBadges()
    {
        return await _db.Badges.Include(x => x.BadgeType).ToListAsync();
    }

    public async Task<List<BadgeType>> GetAllBadgeTypes() => await _db.BadgeTypes.ToListAsync();

    public async Task<AssignedBadge?> GetAssignedBadge(int badgeId)
    {
        return await _db.AssignedBadges
            .Include(x => x.Badge)
            .Include(x => x.FromUser)
            .Include(x => x.User)
            .SingleOrDefaultAsync(x => x.Id == badgeId);
    }

    public async Task<List<AssignedBadge>> GetAssignedBadges(User user)
    {
        return await _db.AssignedBadges.Include(x => x.Badge).Where(x => x.User == user).ToListAsync();
    }

    public async Task<List<AssignedBadge>> GetAssignedBadges(Guid userId)
    {
        return await _db.AssignedBadges.Include(x => x.Badge).Where(x => x.User.PrincipalId == userId).ToListAsync();
    }

    public async Task<List<AssignedBadge>> GetAssignmentHistory(Badge badge)
    {
        return await _db.AssignedBadges
            .Where(x => x.Badge == badge)
            .Include(x => x.User)
            .Include(x => x.FromUser)
            .ToListAsync();
    }

    public async Task<Badge?> GetBadge(int badgeId)
    {
        return await _db.Badges.Include(x => x.BadgeType).SingleOrDefaultAsync(m => m.Id == badgeId);
    }

    public async Task<int> GetBadgeAssignmentCount() => await _db.AssignedBadges.CountAsync();

    public async Task<int> GetBadgeCount() => await _db.Badges.CountAsync();

    public async Task<BadgeType?> GetBadgeType(int badgeId)
    {
        return await _db.BadgeTypes.SingleOrDefaultAsync(x => x.Id == badgeId);
    }

    public async Task<List<AssignedBadge>> GetFullBadgeStream(int limit)
    {
        return await _db.AssignedBadges
            .OrderByDescending(x => x.DateAssigned)
            .Take(limit)
            .Include(x => x.User)
            .Include(x => x.FromUser)
            .Include(x => x.Badge)
            .ToListAsync();
    }

    public IEnumerable<(int place, User? User, int BadgeCount)> GetTopBadgeGivers()
    {
        var query =
            _db.AssignedBadges
                .Include(x => x.User)
                .GroupBy(x => x.FromUserId)
                .Select(group => new
                {
                    User = group.First().FromUser, BadgeCount = group.Count()
                })
                .OrderByDescending(x => x.BadgeCount)
                .Take(10)
                .ToList();

        //zip adds in the place number
        return query.Zip(Enumerable.Range(1, 10000), (o, i) => (i, o.User, o.BadgeCount));
    }

    public IEnumerable<(int place, User? User, int BadgeCount)> GetTopBadgeHolders()
    {
        var query =
            _db.AssignedBadges
                .Include(x => x.User)
                .GroupBy(x => x.UserId)
                .Select(group => new
                {
                    group.First().User, BadgeCount = group.Count()
                })
                .OrderByDescending(x => x.BadgeCount)
                .Take(10)
                .ToList();

        //zip adds in the place number
        return query.Zip(Enumerable.Range(1, 10000), (o, i) => (i, o.User, o.BadgeCount));
    }

    public async Task SaveBadge(Badge badge, AssignedBadge initialAssignment)
    {
        _db.Badges.Add(badge);
        _db.AssignedBadges.Add(initialAssignment);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateBadge(Badge badge)
    {
        _db.Attach(badge).State = EntityState.Modified;
        await _db.SaveChangesAsync();
    }
}