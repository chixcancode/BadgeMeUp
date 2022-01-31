﻿#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BadgeMeUp.Models;
using BadgeMeUp.Db;

namespace BadgeMeUp.Pages.Badges
{
    public class CreateModel : PageModel
    {
        private readonly BadgeDb _badgeDb;
        private readonly UserDb _userDb;

        [BindProperty]
        public Badge Badge { get; set; }

        public List<BadgeType>? BadgeTypes { get; set; }

        public CreateModel(BadgeDb badgeDb, UserDb userDb)
        {
            _badgeDb = badgeDb;
            _userDb = userDb;

            Badge = new Badge();
        }

        public async Task<IActionResult> OnGet()
        {
            BadgeTypes = await _badgeDb.GetAllBadgeTypes();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int badgeTypeId)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //todo: replace with current user
            var currentUser = await _userDb.GetUserById(1);

            Badge.BadgeType = await _badgeDb.GetBadgeType(badgeTypeId);

            var assignment = new AssignedBadge(Badge, currentUser, currentUser);

            await _badgeDb.SaveBadge(Badge, assignment);

            return RedirectToPage("./Index");
        }
    }
}
