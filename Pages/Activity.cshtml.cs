using BadgeMeUp.Db;
using BadgeMeUp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BadgeMeUp.Pages;

public class ActivityModel : PageModel
{
    private readonly BadgeDb _badgeDb;

    private readonly ILogger<IndexModel> _logger;

    private List<AssignedBadge>? _badgeAssignments;

    public ActivityModel(ILogger<IndexModel> logger, BadgeDb badgeDb)
    {
        _logger = logger;
        _badgeDb = badgeDb;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        _badgeAssignments = await _badgeDb.GetFullBadgeStream(100);

        return Page();
    }
}