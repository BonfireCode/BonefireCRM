using BonefireCRM.Integration.Tests.DataSeeders;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace BonefireCRM.Integration.Tests.Common
{
    internal class TestAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public TestAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger, UrlEncoder encoder)
            : base(options, logger, encoder)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var appUser = AppUserTestsDataSeeder.AppUser;

            var claims = new[] 
            { 
                new Claim(ClaimTypes.NameIdentifier, appUser.Id.ToString()),
                new Claim(ClaimTypes.Name, appUser.UserName!),
            };
            var identity = new ClaimsIdentity(claims, TestConstants.AUTHSCHEMA);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, TestConstants.AUTHSCHEMA);

            var result = AuthenticateResult.Success(ticket);

            return Task.FromResult(result);
        }
    }
}
