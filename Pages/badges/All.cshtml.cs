#nullable disable
using BadgeMeUp.Db;
using BadgeMeUp.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BadgeMeUp.Pages.Badges;

public class AllModel : PageModel
{
    private readonly BadgeContext _context;

    public AllModel(BadgeContext context)
    {
        _context = context;
    }

    public IList<Badge> Badge { get; set; }

    public async Task OnGetAsync()
    {
        Badge = await _context.Badges.Include(x => x.BadgeType).ToListAsync();
    }
}