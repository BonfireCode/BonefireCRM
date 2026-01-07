using BonefireCRM.Infrastructure.Security;
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
        private readonly AppDbContext _appDbContext;

        public TestAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger, UrlEncoder encoder, AppDbContext appDbContext)
            : base(options, logger, encoder)
        {
            _appDbContext = appDbContext;
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var appUser = _appDbContext.Users.SingleOrDefault();

            appUser ??= new ApplicationUser
                {
                    Id = Guid.NewGuid(),
                    UserName = "anonymous",
                    Email = "anonymous@est.com"
                };

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
