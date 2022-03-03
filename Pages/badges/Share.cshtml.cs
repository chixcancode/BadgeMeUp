using BadgeMeUp.Db;
using BadgeMeUp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BadgeMeUp.Pages.Badges
{
    public class ShareModel : PageModel
    {
        private readonly BadgeDb _badgeDb;
        private readonly ICurrentUserInfo _userInfo;

        public Badge BadgeToAward { get; set; }
        public string ShareUrl { get; set; }

        public ShareModel(ICurrentUserInfo userInfo, BadgeDb badgeDb)
        {
            _userInfo = userInfo;
            _badgeDb = badgeDb;
        }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            BadgeToAward = await _badgeDb.GetBadge(id.Value);
            var me = _userInfo.GetPrincipalId();

            ShareUrl = ReceiveModel.CreateShareUrl(id.Value, me, null);

            return Page();
        }
    }
}