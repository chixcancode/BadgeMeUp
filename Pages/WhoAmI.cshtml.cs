using BadgeMeUp.Db;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BadgeMeUp.Pages
{
    public class WhoAmIModel : PageModel
    {
        public string? Name  { get; set; }  
        public Guid PrincipalId { get; set; }

        private readonly ICurrentUserInfo _userInfo;
        private readonly UserDb _userDb;

        public WhoAmIModel(ICurrentUserInfo userInfo, UserDb userDb)
        {
            _userInfo = userInfo;
            _userDb = userDb;
        }

        public async Task OnGet()
        {
            Name = _userInfo.GetPrincipalName();
            PrincipalId = _userInfo.GetPrincipalId();

            if (Name != null)
            {
                //Call this to create the user if it doesn't already exist
                await _userDb.GetOrCreateUser(PrincipalId, Name);
            }
        }
    }
}
