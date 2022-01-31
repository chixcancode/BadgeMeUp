using BadgeMeUp.Db;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BadgeMeUp.Pages
{
    public class BadgeImageModel : PageModel
    {
        private readonly BadgeDb _badgeDb;

        public BadgeImageModel(BadgeDb badgeDb)
        {
            _badgeDb = badgeDb;
        }

        public async Task<IActionResult> OnGet(int id)
        {
            var badge = await _badgeDb.GetBadge(id);

            if (badge == null || badge.BannerImageBytes == null)
            {
                var bytes = System.IO.File.ReadAllBytes("./boring-default.jpg");
                return base.File(bytes, "image/jpeg");
            }

            return base.File(badge.BannerImageBytes, badge.BannerImageContentType);
        }
    }
}
