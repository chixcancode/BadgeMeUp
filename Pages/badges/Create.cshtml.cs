#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BadgeMeUp.Models;
using BadgeMeUp.Db;

namespace BadgeMeUp.Pages.Badges
{
    public class CreateModel : PageModel
    {
        private readonly BadgeContext _context;

        public CreateModel(BadgeContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Badge Badge { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Badges.Add(Badge);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
