// <copyright file="DeleteAssignmentEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Extensions;
using BonefireCRM.Domain.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BonefireCRM.API.Activity.Endpoints.Assignment
{
    public class DeleteAssignmentEndpoint : EndpointWithoutRequest<Results<NoContent, NotFound>>
    {
        private readonly ActivityService _activityService;

        public DeleteAssignmentEndpoint(ActivityService activityService)
        {
            _activityService = activityService;
        }

        public override void Configure()
        {
            Delete("/activity/assignments/{id:guid}");
            Summary(s =>
            {
                s.Summary = "Deletes an existing assignment activity.";
                s.Description = "Deletes a specific assignment identified by its unique GUID.";
                s.Params["id"] = "The unique identifier (GUID) of the assignment to delete.";
                s.AddDeleteResponses("Assignment");
            });
        }

        public override async Task<Results<NoContent, NotFound>> ExecuteAsync(CancellationToken ct)
        {
            var id = Route<Guid>("id");

            var isDeleted = await _activityService.DeleteAssignmentAsync(id, ct);
            if (isDeleted)
            {
                return TypedResults.NoContent();
            }

            return TypedResults.NotFound();
        }
    }
}
