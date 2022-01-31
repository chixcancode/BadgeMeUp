using Microsoft.AspNetCore.Mvc.RazorPages;
using BadgeMeUp.Models;
using BadgeMeUp.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace BadgeMeUp.Pages
{
    public class MyBadgesModel : PageModel
    {
        public List<AssignedBadge>? AssignedBadges { get; set; }

        private readonly BadgeDb _badgeDb;

        public MyBadgesModel(BadgeDb badgeDb)
        {
            _badgeDb = badgeDb;
        }

        public async Task<IActionResult> OnGet()
        {
            AssignedBadges = await _badgeDb.GetAssignedBadges(1);

            return Page();
        }

        public static string EncodeMultilineString(string unencoded)
        {
            return unencoded.ReplaceLineEndings("<br />");
        }
    }
}
