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
        public IFormFile? badgeImage { get; set; }

        public int SelectedBadgeTypeId { get; set; }

        public List<AssignedBadge> BadgeHistory { get; set; }
 
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await Populate(id.Value);

            if (Badge == null)
            {
                return NotFound();
            }
            return Page();
        }

        private async Task Populate(int id)
        {
            Badge = await _badgeDb.GetBadge(id);
            BadgeTypes = await _badgeDb.GetAllBadgeTypes();
            SelectedBadgeTypeId = Badge.BadgeType.Id;

            if(Badge != null)
            {
                BadgeHistory = await _badgeDb.GetAssignmentHistory(Badge);
            }
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int badgeTypeId, IFormFile? badgeImage)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //Grab a copy so that we don't have to update every field
            var updateBadge = await _badgeDb.GetBadge(Badge.Id);
            updateBadge.Name = Badge.Name;
            updateBadge.Description = Badge.Description;
            updateBadge.Criteria = Badge.Criteria;

            if(badgeImage != null)
            {
                updateBadge.BannerImageFileName = badgeImage.FileName;
                var ms = new MemoryStream();
                await badgeImage.CopyToAsync(ms);
                updateBadge.BannerImageBytes = ms.ToArray();
                updateBadge.BannerImageContentType = badgeImage.ContentType;
            }

            var selectedBadgeType = await _badgeDb.GetBadgeType(badgeTypeId);
            updateBadge.BadgeType = selectedBadgeType;
            await _badgeDb.UpdateBadge(updateBadge);

            return RedirectToPage("./All");
        }
    }
}
