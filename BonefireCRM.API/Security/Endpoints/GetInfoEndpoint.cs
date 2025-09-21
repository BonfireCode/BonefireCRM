using BonefireCRM.API.Contrat.Security;
using BonefireCRM.API.Security.Mappers;
using BonefireCRM.Domain.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BonefireCRM.API.Security.Endpoints
{
    public class GetInfoEndpoint : EndpointWithoutRequest<Results<Ok<GetInfoResponse>, ProblemHttpResult>>
    {
        private readonly SecurityService _securityService;

        public GetInfoEndpoint(SecurityService securityService)
        {
            _securityService = securityService;
        }

        public override void Configure()
        {
            Get("/info");
        }

        public override async Task<Results<Ok<GetInfoResponse>, ProblemHttpResult>> ExecuteAsync(CancellationToken ct)
        {
            var claimsPrincipal = User;

            var result = await _securityService.ManageGetUserInfo(claimsPrincipal, ct);

            var response = result.Match<Results<Ok<GetInfoResponse>, ProblemHttpResult>>
            (
                dto => TypedResults.Ok(DtoToResponseMapper.MapToResponse(dto)),
                error => TypedResults.Problem(error.Message, statusCode: StatusCodes.Status404NotFound)
            );

            return response;
        }
    }
}
