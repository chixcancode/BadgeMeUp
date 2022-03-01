using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BadgeMeUp.Pages;

public class MyBadgesModel : PageModel
{
    public Task<IActionResult> OnGet() => Task.FromResult<IActionResult>(RedirectToPage("badges/Index"));
}