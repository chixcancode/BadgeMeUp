using Microsoft.AspNetCore.Mvc.RazorPages;
using BadgeMeUp.Models;
using BadgeMeUp.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BadgeMeUp.Pages
{
    public class MyBadgesModel : PageModel
    {

        public MyBadgesModel()
        {
        }

        public async Task<IActionResult> OnGet()
        {
            return RedirectToPage("badges/Index");
        }
    }
}
