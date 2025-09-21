using BonefireCRM.API.Contrat.Security;
using BonefireCRM.API.Security.Mappers;
using BonefireCRM.Domain.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BonefireCRM.API.Security.Endpoints
{
    public class RefreshEndpoint : Endpoint<RefreshRequest, Results<SignInHttpResult, ChallengeHttpResult>>
    {
        private readonly SecurityService _securityService;

        public RefreshEndpoint(SecurityService securityService)
        {
            _securityService = securityService;
        }

        public override void Configure()
        {
            Post("/refresh");
        }

        public override async Task<Results<SignInHttpResult, ChallengeHttpResult>> ExecuteAsync(RefreshRequest request, CancellationToken ct)
        {
            var refreshDTO = RequestToDtoMapper.MapToDto(request);

            var result = await _securityService.RefreshUserToken(refreshDTO, ct);

            if (!result.IsTokenRefreshed)
            {
                return TypedResults.Challenge();
            }

            return TypedResults.SignIn(result.ClaimsPrincipal, authenticationScheme: result.AuthenticationScheme);
        }
    }
}