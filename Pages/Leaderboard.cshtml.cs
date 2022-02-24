using BadgeMeUp.Db;
using BadgeMeUp.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BadgeMeUp.Pages
{
    public class LeaderboardModel : PageModel
    {
        private readonly BadgeDb _badgeDb;

        public List<(User User, int BadgeCount)> TopUsers;
        public List<(User User, int BadgeCount)> TopGivers;

        public LeaderboardModel(BadgeDb badgeDb)
        {
            _badgeDb = badgeDb;
         }

        public void OnGet()
        {
            TopUsers = _badgeDb.GetTopBadgeHolders().ToList();
            TopGivers = _badgeDb.GetTopBadgeGivers().ToList();
        }
    }
}
