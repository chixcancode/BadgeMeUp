using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using System.Security.Claims;
using Microsoft.Identity.Web;

namespace BadgeMeUp.Pages
{
    public class WhoAmIModel : PageModel
    {
        public string Name  { get; set; }  
        public string PrincipalId { get; set; }

        public void OnGet()
        {
           Name = HttpContext.Request.Headers["X-MS-CLIENT-PRINCIPAL-NAME"];
           PrincipalId = HttpContext.Request.Headers["X-MS-CLIENT-PRINCIPAL-ID"];
        }
    }
}
