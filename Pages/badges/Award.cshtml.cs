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
        private readonly EmailQueueDb _emailQueueDb;

        public Badge? BadgeToAward { get; set; }
        public List<User>? AllUsers { get; set; }

        public int SelectedUserId { get; set; }

        [BindProperty]
        public string Comments { get; set; }

        public bool ChangeOwner { get; set; } = false;

        private readonly ICurrentUserInfo _currentUserInfo;

        public AwardModel(BadgeDb badgeDb, UserDb userDb, EmailQueueDb emailQueueDb, ICurrentUserInfo currentUserInfo)
        {
            _badgeDb = badgeDb;
            _userDb = userDb;
            _emailQueueDb = emailQueueDb;
            _currentUserInfo = currentUserInfo;
        }

        public async Task<IActionResult> OnGet(int? id, int? badgeId, Guid? from, string? awardMessage)
        {
            if (id != null)
            {
                BadgeToAward = await _badgeDb.GetBadge(id.Value);

                if (BadgeToAward == null)
                {
                    return NotFound();
                }

                AllUsers = _userDb.GetUsersWithoutBadge(BadgeToAward.Id);

                return Page();
            }
            else if(badgeId != null && from != null)
            {
                return await AutoHandler(badgeId.Value, from.Value, awardMessage);
            }
            else
            {
                return NotFound();
            }
        }

        //This is a special URL that can be sent to another user (in the system or not) and
        //it will create the assignment when the recipient accepts the award.
        private async Task<IActionResult> AutoHandler(int badgeId, Guid from, string? awardMessage)
        {
            var badge = await _badgeDb.GetBadge(badgeId);
            var fromUser = await _userDb.GetUser(from);
            var toUser = await _userDb.GetOrCreateUser(_currentUserInfo.GetPrincipalId(), _currentUserInfo.GetPrincipalName());

            if(fromUser != toUser)
            {
                var hasBadge = await _userDb.HasBadge(toUser, badge);
                if (!hasBadge)
                {
                    await _userDb.AssignBadgeToUser(fromUser, toUser, badge, awardMessage ?? "");
                }
            }

            return RedirectToPage("../MyBadges");
        }

        public async Task<IActionResult> OnPostAsync(int? id, Guid selectedUserId, bool changeOwner)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currentUser = await _userDb.GetOrCreateUser(_currentUserInfo.GetPrincipalId(), _currentUserInfo.GetPrincipalName());
            var toUser = await _userDb.GetUser(selectedUserId);
            var badge = await _badgeDb.GetBadge(id.Value);

            if (badge == null)
            {
                return NotFound();
            }

            await _userDb.AssignBadgeToUser(currentUser, toUser, badge, Comments);
            var email = BadgeAwardTemplate.GetEmailQueue(toUser.PrincipalName, badge, currentUser, Comments);

            await _emailQueueDb.QueueEmailToSend(email);

            if (changeOwner)
            {
                await _userDb.RemoveBadgeFromUser(currentUser, badge);
            }

            return RedirectToPage("../MyBadges");
        }
    }
}
