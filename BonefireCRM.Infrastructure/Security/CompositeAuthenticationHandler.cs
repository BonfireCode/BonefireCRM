using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;

public class CompositeAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly IAuthenticationSchemeProvider _schemes;

    public CompositeAuthenticationHandler(
        IAuthenticationSchemeProvider schemes,
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder) : base(options, logger, encoder)
    {
        _schemes = schemes;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var schemeNames = new[] 
        { 
            IdentityConstants.BearerScheme,
            IdentityConstants.ApplicationScheme 
        };

        foreach (var name in schemeNames)
        {
            var result = await Context.AuthenticateAsync(name);
            if (result?.Succeeded == true)
            {
                return AuthenticateResult.Success(result.Ticket);
            }
        }

        return AuthenticateResult.NoResult();
    }
}
