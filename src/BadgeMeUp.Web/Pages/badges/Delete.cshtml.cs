#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BadgeMeUp.Models;
using BadgeMeUp.Db;

namespace BadgeMeUp.Pages.Badges
{
    public class DeleteModel : PageModel
    {
        private readonly BadgeContext _context;
        private readonly BadgeDb _badgeDb;

        public DeleteModel(BadgeContext context, BadgeDb badgeDb)
        {
            _context = context;
            _badgeDb = badgeDb;
        }

        [BindProperty]
        public Badge Badge { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Badge = await _context.Badges.FirstOrDefaultAsync(m => m.Id == id);

            if (Badge == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Badge = await _context.Badges.FindAsync(id);

            if (Badge != null)
            {
                _badgeDb.DeleteBadge(Badge);
            }

            return RedirectToPage("./Index");
        }
    }
}
