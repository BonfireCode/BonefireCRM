using BonefireCRM.API.Contrat.Security;
using BonefireCRM.API.Security.Mappers;
using BonefireCRM.Domain.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BonefireCRM.API.Security.Endpoints
{
    public class ConfirmEmailEndpoint : Endpoint<ConfirmEmailRequest, Results<Ok, UnauthorizedHttpResult>>
    {
        private readonly SecurityService _securityService;

        public ConfirmEmailEndpoint(SecurityService securityService)
        {
            _securityService = securityService;
        }

        public override void Configure()
        {
            Get("/confirmemail");
            AllowAnonymous();
        }

        public override async Task<Results<Ok, UnauthorizedHttpResult>> ExecuteAsync(ConfirmEmailRequest request, CancellationToken ct)
        {
            var confirmEmailDTO = RequestToDtoMapper.MapToDto(request);

            var result = await _securityService.ConfirmUserEmail(confirmEmailDTO, ct);

            var response = result.Match<Results<Ok, UnauthorizedHttpResult>>
            (
                isEmailConfirmed => TypedResults.Ok(),
                _ => TypedResults.Unauthorized()
            );

            return response;
        }
    }
}
