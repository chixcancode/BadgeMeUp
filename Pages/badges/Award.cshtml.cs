using BadgeMeUp.Db;
using BadgeMeUp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace BadgeMeUp.Pages.badges
{
    public class AwardModel : PageModel
    {
        private readonly BadgeDb _badgeDb;
        private readonly UserDb _userDb;

        public Badge? BadgeToAward { get; set; }
        public List<User>? AllUsers { get; set; }

        public int SelectedUserId { get; set; }

        public AwardModel(BadgeDb badgeDb, UserDb userDb)
        {
            _badgeDb = badgeDb;
            _userDb = userDb;
        }

        public async Task<IActionResult> OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            BadgeToAward = await _badgeDb.GetBadge(id.Value);

            if(BadgeToAward == null)
            {
                return NotFound();
            }

            AllUsers = _userDb.GetUsersWithoutBadge(BadgeToAward.Id);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id, int selectedUserId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currentUser = await _userDb.GetUserById(1);
            var toUser = await _userDb.GetUserById(selectedUserId);
            var badge = await _badgeDb.GetBadge(id.Value);

            if(badge == null)
            {
                return NotFound();
            }

            await _userDb.AssignBadgeToUser(currentUser, toUser, badge);

            return RedirectToPage("../MyBadges");
        }
    }
}
