// <copyright file="CreateDealParticipantRoleEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using System.Security.Claims;
using BonefireCRM.API.Contrat.DealParticipantRole;
using BonefireCRM.API.DealParticipantRole.Mappers;
using BonefireCRM.API.Extensions;
using BonefireCRM.Domain.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BonefireCRM.API.DealParticipantRole.Endpoints
{
    public class CreateDealParticipantRoleEndpoint : Endpoint<CreateDealParticipantRoleRequest, Results<Created<CreateDealParticipantRoleResponse>, InternalServerError>>
    {
        private readonly DealParticipantRoleService _dealParticipantRoleService;
        private readonly UserService _userService;

        public CreateDealParticipantRoleEndpoint(DealParticipantRoleService dealParticipantRoleService, UserService userService)
        {
            _dealParticipantRoleService = dealParticipantRoleService;
            _userService = userService;
        }

        public override void Configure()
        {
            Post("/dealparticipantroles");

            Summary(s =>
            {
                s.Summary = "Creates a new deal participant role.";
                s.AddCreateResponses<CreateDealParticipantRoleResponse>("Deal participant role");
            });
        }

        public override async Task<Results<Created<CreateDealParticipantRoleResponse>, InternalServerError>> ExecuteAsync(CreateDealParticipantRoleRequest request, CancellationToken ct)
        {
            var registerId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var userId = await _userService.GetUserIdAsync(registerId, ct);

            var dtoDealParticipantRole = request.MapToDto(userId);

            var result = await _dealParticipantRoleService.CreateDealParticipantRoleAsync(dtoDealParticipantRole, ct);

            var response = result.Match<Results<Created<CreateDealParticipantRoleResponse>, InternalServerError>>(
                createdDealParticipantRole => TypedResults.Created($"/dealparticipantroles/{createdDealParticipantRole.Id}", createdDealParticipantRole.MapToResponse()),
                _ => TypedResults.InternalServerError());

            return response;
        }
    }
}
