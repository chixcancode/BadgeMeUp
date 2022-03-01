#nullable disable
using BadgeMeUp.Db;
using BadgeMeUp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BadgeMeUp.Pages.Badges;

public class DeleteModel : PageModel
{
    private readonly BadgeDb _badgeDb;

    private readonly BadgeContext _context;

    public DeleteModel(BadgeContext context, BadgeDb badgeDb)
    {
        _context = context;
        _badgeDb = badgeDb;
    }

    [BindProperty]
    public Badge Badge { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if(id == null)
        {
            return NotFound();
        }

        Badge = await _context.Badges.FirstOrDefaultAsync(m => m.Id == id);

        if(Badge == null)
        {
            return NotFound();
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if(id == null)
        {
            return NotFound();
        }

        Badge = await _context.Badges.FindAsync(id);

        if(Badge != null)
        {
            _badgeDb.DeleteBadge(Badge);
        }

        return RedirectToPage("./Index");
    }
}