using Microsoft.AspNetCore.Mvc;

namespace BadgeMeUp
{
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
            if (id is null)
            {
                return Guid.Empty;
            }
         
            return Guid.Parse(id);
        }

        public string? GetPrincipalName()
        {
            return _httpContext.HttpContext?.Request.Headers["X-MS-CLIENT-PRINCIPAL-NAME"];
        }
    }
}
