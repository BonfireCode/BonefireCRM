using System.ComponentModel;

namespace BonefireCRM.API.Contrat.Security
{
    public sealed class RefreshRequest
    {
        [Description("The refresh token from the last '/login' or '/refresh' response, used to obtain a new access token with an extended expiration.")]
        public required string RefreshToken { get; init; }
    }
}
