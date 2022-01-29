using Microsoft.AspNetCore.Mvc.RazorPages;
using BadgeMeUp.Models;
using BadgeMeUp.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace BadgeMeUp.Pages
{
    public class MyBadgesModel : PageModel
    {
        public User? SelectedUser = null;

        public IActionResult OnGet(string? alias)
        {
            if(alias == null)
            {
                alias = "jayoung";
            }

            using (var db = new BadgeContext())
            {
                var user = db.Users?
                    .Include(x => x.AssignedBadges)
                    .ThenInclude(x => x.Badge)
                    .FirstOrDefault(x => x.Alias == alias);

                if (user == null)
                {
                    return NotFound();
                }

                Console.WriteLine(user.AssignedBadges.Count());

                if (user != null)
                {
                    this.SelectedUser = user;
                }

                return Page();
            }
        }
    }
}
