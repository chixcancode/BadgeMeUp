using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BadgeMeUp.Pages
{
    public class WhoAmIModel : PageModel
    {
        public string? Name  { get; set; }  
        public Guid PrincipalId { get; set; }

        private readonly ICurrentUserInfo _userInfo;

        public WhoAmIModel(ICurrentUserInfo userInfo)
        {
            _userInfo = userInfo;
        }

        public void OnGet()
        {
            Name = _userInfo.GetPrincipalName();
            PrincipalId = _userInfo.GetPrincipalId();
        }
    }
}
