namespace BadgeMeUp;

public class AppServiceCurrentUser : ICurrentUserInfo
{
    private readonly IHttpContextAccessor _httpContext;

    public AppServiceCurrentUser(IHttpContextAccessor httpContext)
    {
        _httpContext = httpContext;
    }

    public Guid GetPrincipalId()
    {
        var id = _httpContext.HttpContext?.Request.Headers["X-MS-CLIENT-PRINCIPAL-ID"];
        return id is null ? Guid.Empty : Guid.Parse(id);
    }

    public string? GetPrincipalName() => _httpContext.HttpContext?.Request.Headers["X-MS-CLIENT-PRINCIPAL-NAME"];
}