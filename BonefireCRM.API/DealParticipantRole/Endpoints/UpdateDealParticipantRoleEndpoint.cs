// <copyright file="UpdateDealParticipantRoleEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contrat.DealParticipantRole;
using BonefireCRM.API.DealParticipantRole.Mappers;
using BonefireCRM.API.Extensions;
using BonefireCRM.Domain.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Security.Claims;

namespace BonefireCRM.API.DealParticipantRole.Endpoints
{
    public class UpdateDealParticipantRoleEndpoint : Endpoint<UpdateDealParticipantRoleRequest, Results<Ok<UpdateDealParticipantRoleResponse>, NotFound, InternalServerError>>
    {
        private readonly DealParticipantRoleService _contactService;
        private readonly UserService _userService;

        public UpdateDealParticipantRoleEndpoint(DealParticipantRoleService contactService, UserService userService)
        {
            _contactService = contactService;
            _userService = userService;
        }

        public override void Configure()
        {
            Put("/dealparticipantroles/{id:guid}");

            Summary(s =>
            {
                s.Summary = "Updates an existing deal participant role.";
                s.Description = "Updates the details of an existing deal participant role identified by its unique ID.";
                s.Params["id"] = "The unique identifier (GUID) of the deal participant role to update.";
                s.AddUpdateResponses<UpdateDealParticipantRoleResponse>("Deal participant role");
            });
        }

        public override async Task<Results<Ok<UpdateDealParticipantRoleResponse>, NotFound, InternalServerError>> ExecuteAsync(UpdateDealParticipantRoleRequest request, CancellationToken ct)
        {
            var registerId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var userId = await _userService.GetUserIdAsync(registerId, ct);

            var id = Route<Guid>("id");

            var dtoDealParticipantRole = request.MapToDto(id, userId);

            var result = await _contactService.UpdateDealParticipantRoleAsync(dtoDealParticipantRole, ct);

            var response = result.Match<Results<Ok<UpdateDealParticipantRoleResponse>, NotFound, InternalServerError>>(
                updatedDealParticipantRole => TypedResults.Ok(updatedDealParticipantRole.MapToResponse()),
                _ => TypedResults.InternalServerError());

            return response;
        }
    }
}
