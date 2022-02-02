using BadgeMeUp.Db;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BadgeMeUp.Pages
{
    public class WhoAmIModel : PageModel
    {
        public string? Name  { get; set; }  
        public Guid PrincipalId { get; set; }
        public IHeaderDictionary Headers { get; set; }

        private readonly ICurrentUserInfo _userInfo;
        private readonly UserDb _userDb;

        private readonly IHttpContextAccessor _contextAccessor;

        public WhoAmIModel(ICurrentUserInfo userInfo, UserDb userDb, IHttpContextAccessor context)
        {
            _userInfo = userInfo;
            _userDb = userDb;

            _contextAccessor = context;
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

            Headers = _contextAccessor.HttpContext.Request.Headers;
        }
    }
}
