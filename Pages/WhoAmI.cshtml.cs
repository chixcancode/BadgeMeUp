using BadgeMeUp.Db;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BadgeMeUp.Pages;

public class WhoAmIModel : PageModel
{
    private readonly IHttpContextAccessor _contextAccessor;

    private readonly UserDb _userDb;

    private readonly ICurrentUserInfo _userInfo;

    public WhoAmIModel(ICurrentUserInfo userInfo, UserDb userDb, IHttpContextAccessor context, IHeaderDictionary? headers)
    {
        _userInfo = userInfo;
        _userDb = userDb;

        _contextAccessor = context;
        Headers = headers;
    }

    public IHeaderDictionary? Headers { get; private set; }

    public string? Name { get; private set; }

    public Guid PrincipalId { get; private set; }

    public async Task OnGet()
    {
        Name = _userInfo.GetPrincipalName();
        PrincipalId = _userInfo.GetPrincipalId();

        if(Name != null)
        {
            //Call this to create the user if it doesn't already exist
            await _userDb.GetOrCreateUser(PrincipalId, Name);
        }

        Headers = _contextAccessor.HttpContext?.Request.Headers;
    }
}