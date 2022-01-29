using BadgeMeUp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BadgeMeUp.Pages.badges
{
    public class AwardModel : PageModel
    {
        BadgeContext _dbContext;

        public Badge? BadgeToAward { get; set; }
        public List<User>? AllUsers { get; set; }

        public AwardModel(BadgeContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            BadgeToAward = _dbContext.Badges?.Single(x => x.Id == id);
            AllUsers = _dbContext.Users?.OrderBy(x => x.Alias).ToList();
            
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id, int selectedUserId)
        {
            var newAssignment = new AssignedBadge();

            newAssignment.Badge = _dbContext.Badges?.Single(x => x.Id == id);

            //Todo: use the current user
            newAssignment.FromUser = _dbContext.Users.First();
            newAssignment.User = _dbContext.Users.Single(x => x.Id == selectedUserId);
            newAssignment.DateAssigned = DateTime.UtcNow;

            _dbContext.AssignedBadges.Add(newAssignment);
            await _dbContext.SaveChangesAsync();


            return RedirectToPage("../MyBadges");
        }
    }
}
