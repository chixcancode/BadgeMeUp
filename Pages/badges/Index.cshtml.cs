using BadgeMeUp.Db;
using BadgeMeUp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BadgeMeUp.Pages.Badges;

public class IndexModel : PageModel
{
    private readonly BadgeDb _badgeDb;

    private readonly ICurrentUserInfo _currentUserInfo;

    private readonly UserDb _userDb;

    public IndexModel(BadgeDb badgeDb, UserDb userDb, ICurrentUserInfo currentUserInfo)
    {
        _badgeDb = badgeDb;
        _userDb = userDb;
        _currentUserInfo = currentUserInfo;
    }

    public List<AssignedBadge>? AssignedBadges { get; set; }

    public string? OtherUser { get; set; }

    public Guid UserId { get; set; }

    public static string EncodeMultilineString(string unencoded) => unencoded.ReplaceLineEndings("<br />");

    public async Task<IActionResult> OnGet(Guid? id)
    {
        var currentUser = await _userDb.GetOrCreateUser(_currentUserInfo.GetPrincipalId(), _currentUserInfo.GetPrincipalName());

        if(id == null || currentUser.PrincipalId == id.Value)
        {
            //Not viewing someone else

            UserId = currentUser.PrincipalId;

            AssignedBadges = await _badgeDb.GetAssignedBadges(currentUser);
            OtherUser = null;
        }
        else
        {
            var user = await _userDb.GetUser(id.Value);

            AssignedBadges = await _badgeDb.GetAssignedBadges(id.Value);

            OtherUser = user.PrincipalName;

            if(user == null)
            {
                return NotFound();
            }

            UserId = id.Value;
        }

        return Page();
    }
}