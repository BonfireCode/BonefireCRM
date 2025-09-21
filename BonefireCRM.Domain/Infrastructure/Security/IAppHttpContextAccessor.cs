using System.Security.Principal;

namespace BonefireCRM.Domain.Infrastructure.Security
{
    public interface IAppHttpContextAccessor
    {
        IPrincipal GetCurrentUser();

        string GetBaseUrl();
    }
}
