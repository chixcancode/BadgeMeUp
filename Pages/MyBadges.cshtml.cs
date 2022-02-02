using Microsoft.AspNetCore.Mvc.RazorPages;
using BadgeMeUp.Models;
using BadgeMeUp.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BadgeMeUp.Pages
{
    public class MyBadgesModel : PageModel
    {
        public List<AssignedBadge>? AssignedBadges { get; set; }

        private readonly BadgeDb _badgeDb;
        private readonly UserDb _userDb;
        private readonly ICurrentUserInfo _currentUserInfo;

        public MyBadgesModel(BadgeDb badgeDb, UserDb userDb, ICurrentUserInfo currentUserInfo)
        {
            _badgeDb = badgeDb;
            _userDb = userDb;
            _currentUserInfo = currentUserInfo;
        }

        public async Task<IActionResult> OnGet()
        {
            var user = await _userDb.GetOrCreateUser(_currentUserInfo.GetPrincipalId(), _currentUserInfo.GetPrincipalName());

            AssignedBadges = await _badgeDb.GetAssignedBadges(user);

            return Page();
        }

        public static string EncodeMultilineString(string unencoded)
        {
            return unencoded.ReplaceLineEndings("<br />");
        }
    }
}
