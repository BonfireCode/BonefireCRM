using BonefireCRM.API.Contrat.Security;
using BonefireCRM.API.Security.Mappers;
using BonefireCRM.Domain.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BonefireCRM.API.Security.Endpoints
{
    public class TwoFactorEndpoint : Endpoint<TwoFactorRequest, Results<Ok<TwoFactorResponse>, ProblemHttpResult>>
    {
        private readonly SecurityService _securityService;

        public TwoFactorEndpoint(SecurityService securityService)
        {
            _securityService = securityService;
        }

        public override void Configure()
        {
            Post("/2fa");
        }

        public override async Task<Results<Ok<TwoFactorResponse>, ProblemHttpResult>> ExecuteAsync(TwoFactorRequest request, CancellationToken ct)
        {
            var claimsPrincipal = User;

            var twoFactorDTO = RequestToDtoMapper.MapToDto(request);

            var result = await _securityService.ManageUserTwoFactor(twoFactorDTO, claimsPrincipal, ct);

            var response = result.Match<Results<Ok<TwoFactorResponse>, ProblemHttpResult>>
            (
                dto => TypedResults.Ok(DtoToResponseMapper.MapToResponse(dto)),
                error => TypedResults.Problem(error.Message)
            );

            return response;
        }
    }
}
