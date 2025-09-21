using BonefireCRM.API.Contrat.Security;
using BonefireCRM.API.Security.Mappers;
using BonefireCRM.Domain.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BonefireCRM.API.Security.Endpoints
{
    public class ResendConfirmationEndpoint : Endpoint<ResendConfirmationRequest, Ok>
    {
        private readonly SecurityService _securityService;

        public ResendConfirmationEndpoint(SecurityService securityService)
        {
            _securityService = securityService;
        }

        public override void Configure()
        {
            Post("/resendconfirmation");
            AllowAnonymous();
        }

        public override async Task<Ok> ExecuteAsync(ResendConfirmationRequest request, CancellationToken ct)
        {
            var resendConfirmationDTO = RequestToDtoMapper.MapToDto(request);

            await _securityService.ResendUserConfirmation(resendConfirmationDTO, ct);

            return TypedResults.Ok();
        }
    }
}
