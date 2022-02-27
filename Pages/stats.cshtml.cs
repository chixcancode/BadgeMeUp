using BadgeMeUp.Db;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BadgeMeUp.Pages
{
    public class statsModel : PageModel
    {
        public int NumberOfUsers { get; set; }
        public int NumberOfBadges { get; set; }
        public int NumberOfBadgeAssignments { get; set; }

        private readonly BadgeDb _badgeDb;
        private readonly UserDb _userDb;

        public statsModel(BadgeDb badgeDb, UserDb userDb)
        {
            _badgeDb = badgeDb;
            _userDb = userDb;
        }

        public async Task OnGetAsync()
        {
            NumberOfUsers = await _userDb.GetNumberOfUsers();
            NumberOfBadges = await _badgeDb.GetBadgeCount();    
            NumberOfBadgeAssignments = await _badgeDb.GetBadgeAssignmentCount();

        }
    }
}
