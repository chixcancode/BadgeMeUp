using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using System.Security.Claims;

namespace BadgeMeUp.Pages
{
    public class WhoAmIModel : PageModel
    {
        public string Name  { get; set; }  

        public void OnGet()
        {
            if(ClaimsPrincipal.Current != null)
            {
                Name = ClaimsPrincipal.Current.Identity.Name;
            }
        }
    }
}
