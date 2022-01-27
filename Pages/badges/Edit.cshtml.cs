#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BadgeMeUp;
using BadgeMeUp.Models;

namespace BadgeMeUp.Pages.Badges
{
    public class EditModel : PageModel
    {
        private readonly BadgeMeUp.BadgeContext _context;

        public EditModel(BadgeMeUp.BadgeContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Badge Badge { get; set; }
        [BindProperty]
        public List<BadgeType> BadgeTypes { get; set; }

        public int SelectedBadgeTypeId { get; set; }
 
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Badge = await _context.Badges.Include(x => x.BadgeType).FirstOrDefaultAsync(m => m.Id == id);
            BadgeTypes = await _context.BadgeTypes.ToListAsync();
            SelectedBadgeTypeId = Badge.BadgeType.Id;

            if (Badge == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int badgeTypeId)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var selectedBadgeType = _context.BadgeTypes.Single(x => x.Id == badgeTypeId);
            Badge.BadgeType = selectedBadgeType;
            _context.Attach(Badge).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BadgeExists(Badge.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool BadgeExists(int id)
        {
            return _context.Badges.Any(e => e.Id == id);
        }
    }
}
