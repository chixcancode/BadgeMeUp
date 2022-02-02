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

        [BindProperty]
        public string Comments { get; set; }

        public bool ChangeOwner { get; set; } = false;

        private readonly ICurrentUserInfo _currentUserInfo;

        public AwardModel(BadgeDb badgeDb, UserDb userDb, ICurrentUserInfo currentUserInfo)
        {
            _badgeDb = badgeDb;
            _userDb = userDb;
            _currentUserInfo = currentUserInfo;
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

        public async Task<IActionResult> OnPostAsync(int? id, Guid selectedUserId, bool changeOwner)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currentUser = await _userDb.GetUserByPrincipalGuid(_currentUserInfo.GetPrincipalId());
            var toUser = await _userDb.GetUserByPrincipalGuid(selectedUserId);
            var badge = await _badgeDb.GetBadge(id.Value);

            if(badge == null)
            {
                return NotFound();
            }

            await _userDb.AssignBadgeToUser(currentUser, toUser, badge, Comments);

            if (changeOwner)
            {
                await _userDb.RemoveBadgeFromUser(currentUser, badge);
            }

            return RedirectToPage("../MyBadges");
        }
    }
}
