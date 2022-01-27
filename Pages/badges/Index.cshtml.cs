#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BadgeMeUp;
using BadgeMeUp.Models;

namespace BadgeMeUp.Pages.Badges
{
    public class IndexModel : PageModel
    {
        private readonly BadgeMeUp.BadgeContext _context;

        public IndexModel(BadgeMeUp.BadgeContext context)
        {
            _context = context;
        }

        public IList<Badge> Badge { get;set; }

        public async Task OnGetAsync()
        {
            Badge = await _context.Badges.Include(x => x.BadgeType).ToListAsync();
        }
    }
}
