using BadgeMeUp.Db;
using BadgeMeUp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BadgeMeUp.Pages.badges;

public class DeleteShareModel : PageModel
{
    private readonly BadgeDb _badgeDb;

    public AssignedBadge AssignedBadge { get; set; }

    public DeleteShareModel(BadgeDb badgeDb)
    {
        _badgeDb = badgeDb;
    }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if(id == null)
        {
            return NotFound();
        }

        AssignedBadge = await _badgeDb.GetAssignedBadge(id.Value);

        if(AssignedBadge == null)
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

        AssignedBadge = await _badgeDb.GetAssignedBadge(id.Value);
        await _badgeDb.DeleteAssignedBadge(id.Value);

        return RedirectToPage("Edit", new
        {
            id = AssignedBadge.Badge.Id
        });
    }
}