using BadgeMeUp.Db;
using BadgeMeUp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BadgeMeUp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly BadgeDb _badgeDb;

        public List<AssignedBadge>? BadgeAssignments;

        public IndexModel(ILogger<IndexModel> logger, BadgeDb badgeDb)
        {
            _logger = logger;
            _badgeDb = badgeDb;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            BadgeAssignments = await _badgeDb.GetFullBadgeStream(100);

            return Page();
        }
    }
}