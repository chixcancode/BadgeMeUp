using BadgeMeUp.Db;
using BadgeMeUp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BadgeMeUp.Pages.Badges;

public class ReceiveModel : PageModel
{
    private readonly BadgeDb _badgeDb;

    private readonly ICurrentUserInfo _currentUserInfo;

    private readonly UserDb _userDb;

    public ReceiveModel(BadgeDb badgeDb, UserDb userDb, ICurrentUserInfo userLookup)
    {
        _badgeDb = badgeDb;
        _userDb = userDb;
        _currentUserInfo = userLookup;
    }

    public string? AwardMessage { get; set; }

    public Badge? Badge { get; set; }

    public User? FromUser { get; set; }

    public static string CreateShareUrl(int badgeId, Guid userId, string awardMessage)
    {
        if(awardMessage == null)
        {
            awardMessage = string.Empty;
        }

        return string.Format("https://badgemeup.azurewebsites.net/badges/receive?id={0}&fromUserId={1}&awardMessage={2}",
            badgeId, userId.ToString(), Uri.EscapeDataString(awardMessage));
    }

    public async Task<IActionResult> OnGetAsync(int? id, Guid fromUserId, string? awardMessage)
    {
        if(id == null)
        {
            return NotFound();
        }

        Badge = await _badgeDb.GetBadge(id.Value);
        FromUser = await _userDb.GetUser(fromUserId);

        if(FromUser == null)
        {
            return NotFound();
        }

        AwardMessage = awardMessage;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id, Guid fromUserId, string? awardMessage)
    {
        var badge = await _badgeDb.GetBadge(id.Value);
        var fromUser = await _userDb.GetUser(fromUserId);
        var toUser = await _userDb.GetOrCreateUser(_currentUserInfo.GetPrincipalId(), _currentUserInfo.GetPrincipalName());

        if(fromUser != toUser)
        {
            var hasBadge = await _userDb.HasBadge(toUser, badge);
            if(!hasBadge)
            {
                await _userDb.AssignBadgeToUser(fromUser, toUser, badge, awardMessage ?? "");
            }
        }

        return RedirectToPage("./Index");
    }
}