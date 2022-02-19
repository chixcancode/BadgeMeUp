using BadgeMeUp.Db;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BadgeMeUp.Pages
{
    public class MigrateImagesModel : PageModel
    {
        private readonly BadgeDb _badgeDb;
        private readonly BadgeImageDb _badgeImageDb;

        public MigrateImagesModel(BadgeDb badgeDb, BadgeImageDb badgeImageDb)
        {
            _badgeDb = badgeDb;
            _badgeImageDb = badgeImageDb;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var allBadges = await _badgeDb.GetAllBadges();

            foreach(var badge in allBadges)
            {
                var ms = new MemoryStream(badge.BannerImageBytes);
                await _badgeImageDb.SaveBadgeImage(badge.Id, ms, badge.BannerImageContentType);

                var newUrl = _badgeImageDb.GetBadgeImageUrl(badge.Id);
                badge.BadgeStorageUrl = newUrl.AbsoluteUri;
                await _badgeDb.UpdateBadge(badge);
            }


            return Page();
        }
    }
}
