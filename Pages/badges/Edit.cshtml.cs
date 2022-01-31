using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BadgeMeUp.Models;
using BadgeMeUp.Db;
using System.Drawing;

namespace BadgeMeUp.Pages.Badges
{
    public class EditModel : PageModel
    {
        private readonly BadgeDb _badgeDb;

        public EditModel(BadgeDb badgeDb)
        {
            _badgeDb = badgeDb;
        }

        [BindProperty]
        public Badge? Badge { get; set; }
        [BindProperty]
        public List<BadgeType>? BadgeTypes { get; set; }

        [BindProperty]
        public IFormFile badgeImage { get; set; }

        public int SelectedBadgeTypeId { get; set; }
 
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Badge = await _badgeDb.GetBadge(id.Value);
            BadgeTypes = await _badgeDb.GetAllBadgeTypes();
            SelectedBadgeTypeId = Badge.BadgeType.Id;

            if (Badge == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int badgeTypeId)
        {
            return await OnPostAsync(badgeTypeId, null);
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int badgeTypeId, IFormFile? badgeImage)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if(badgeImage != null)
            {
                Badge.BannerImageFileName = badgeImage.FileName;
                var ms = new MemoryStream();
                await badgeImage.CopyToAsync(ms);
                Badge.BannerImageBytes = ms.ToArray();
                Badge.BannerImageContentType = badgeImage.ContentType;
            }

            var selectedBadgeType = await _badgeDb.GetBadgeType(badgeTypeId);
            Badge.BadgeType = selectedBadgeType;
            await _badgeDb.UpdateBadge(Badge);

            return RedirectToPage("./Index");
        }
    }
}
