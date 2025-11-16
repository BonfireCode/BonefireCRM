// <copyright file="GetDealParticipantRoleEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using System.Security.Claims;
using BonefireCRM.API.Contrat.DealParticipantRole;
using BonefireCRM.API.Contrat.LifeCycleStage;
using BonefireCRM.API.DealParticipantRole.Mappers;
using BonefireCRM.API.Extensions;
using BonefireCRM.Domain.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BonefireCRM.API.DealParticipantRole.Endpoints
{
    public class GetDealParticipantRoleEndpoint : EndpointWithoutRequest<Results<Ok<GetDealParticipantRoleResponse>, NotFound>>
    {
        private readonly DealParticipantRoleService _dealParticipantRoleService;
        private readonly UserService _userService;

        public GetDealParticipantRoleEndpoint(DealParticipantRoleService DealParticipantRoleService, UserService userService)
        {
            _dealParticipantRoleService = DealParticipantRoleService;
            _userService = userService;
        }

        public override void Configure()
        {
            Get("/dealparticipantroles/{id:guid}");
            Summary(s =>
            {
                s.Summary = "Retrieves a specific deal participant role by ID.";
                s.Description = "Fetches detailed information about a deal participant roles identified by its unique ID.";

                s.Params["id"] = "The unique identifier (GUID) of the deal participant roles to retrieve.";

                s.AddGetResponses<GetLifecycleStageResponse>("Deal Participant Role");
            });
        }

        public override async Task<Results<Ok<GetDealParticipantRoleResponse>, NotFound>> ExecuteAsync(CancellationToken ct)
        {
            var id = Route<Guid>("id");

            var registerId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var userId = await _userService.GetUserIdAsync(registerId, ct);

            var result = await _dealParticipantRoleService.GetDealParticipantRoleAsync(id, userId, ct);

            var response = result.Match<Results<Ok<GetDealParticipantRoleResponse>, NotFound>>(
                dtoDealParticipantRole => TypedResults.Ok(dtoDealParticipantRole.MapToResponse()),
                TypedResults.NotFound());

            return response;
        }
    }
}
