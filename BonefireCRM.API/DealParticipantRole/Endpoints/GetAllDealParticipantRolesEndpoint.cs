// <copyright file="GetAllDealParticipantRolesEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using System.Security.Claims;
using BonefireCRM.API.Contrat.DealParticipantRole;
using BonefireCRM.API.DealParticipantRole.Mappers;
using BonefireCRM.API.Extensions;
using BonefireCRM.Domain.Services;
using FastEndpoints;

namespace BonefireCRM.API.DealParticipantRole.Endpoints
{
    public class GetAllDealParticipantRolesEndpoint : Endpoint<GetDealParticipantRolesRequest, IEnumerable<GetDealParticipantRoleResponse>>
    {
        private readonly DealParticipantRoleService _dealParticipantRoleService;
        private readonly UserService _userService;

        public GetAllDealParticipantRolesEndpoint(DealParticipantRoleService dealParticipantRoleService, UserService userService)
        {
            _dealParticipantRoleService = dealParticipantRoleService;
            _userService = userService;
        }

        public override void Configure()
        {
            Get("/dealparticipantroles");
            Summary(s =>
            {
                s.Summary = "Retrieves all specific deal participant roles";
                s.Description = "Fetches detailed information about deal participant roles";

                s.AddGetAllResponses<IEnumerable<GetDealParticipantRoleResponse>>("Deal Participant Roles");
            });
        }

        public override async Task<IEnumerable<GetDealParticipantRoleResponse>> ExecuteAsync(GetDealParticipantRolesRequest request, CancellationToken ct)
        {
            var registerId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var userId = await _userService.GetUserIdAsync(registerId, ct);

            var dtoDealParticipantRoles = request.MapToDto(userId);

            var result = _dealParticipantRoleService.GetAllParticipantRoles(dtoDealParticipantRoles, ct);

            var response = result.Select(c => c.MapToResponse());

            return await Task.Run(() => response);
        }
    }
}
