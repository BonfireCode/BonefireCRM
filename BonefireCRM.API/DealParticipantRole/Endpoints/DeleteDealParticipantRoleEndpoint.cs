// <copyright file="DeleteDealParticipantRoleEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Extensions;
using BonefireCRM.Domain.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BonefireCRM.API.DealParticipantRole.Endpoints
{
    public class DeleteDealParticipantRoleEndpoint : EndpointWithoutRequest<Results<NoContent, NotFound>>
    {
        private readonly DealParticipantRoleService _dealParticipantRoleService;

        public DeleteDealParticipantRoleEndpoint(DealParticipantRoleService dealParticipantRoleService)
        {
            _dealParticipantRoleService = dealParticipantRoleService;
        }

        public override void Configure()
        {
            Delete("/dealparticipantroles/{id:guid}");

            Summary(s =>
            {
                s.Summary = "Deletes an existing deal participant role.";
                s.Description = "Removes a deal participant role record identified by its unique ID. Returns 204 if deleted successfully, or 404 if the call was not found.";

                s.Params["id"] = "The unique identifier (GUID) of the deal participant role to delete.";

                s.AddDeleteResponses("DealParticipantRole");
            });
        }

        public override async Task<Results<NoContent, NotFound>> ExecuteAsync(CancellationToken ct)
        {
            var id = Route<Guid>("id");

            var isDeleted = await _dealParticipantRoleService.DeleteDealParticipantRoleAsync(id, ct);
            if (isDeleted)
            {
                return TypedResults.NoContent();
            }

            return TypedResults.NotFound();
        }
    }
}
