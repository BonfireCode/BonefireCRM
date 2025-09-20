using BonefireCRM.Domain.Infrastructure.Security;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Security.Principal;

namespace BonefireCRM.Infrastructure.Security
{
    internal class AppHttpContextAccessor : IAppHttpContextAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AppHttpContextAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public IPrincipal GetCurrentUser()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext is null)
            {
                return new ClaimsPrincipal();
            }

            return _httpContextAccessor.HttpContext!.User;
        }

        public string GetBaseUrl()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext is null)
            {
                return string.Empty;
            }

            return $"{httpContext.Request.Scheme}://{httpContext.Request.Host}";
        }

    }
}
