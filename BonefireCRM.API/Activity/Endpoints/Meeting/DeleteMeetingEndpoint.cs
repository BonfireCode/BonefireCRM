// <copyright file="DeleteMeetingEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Extensions;
using BonefireCRM.Domain.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BonefireCRM.API.Activity.Endpoints.Meeting
{
    public class DeleteMeetingEndpoint : EndpointWithoutRequest<Results<NoContent, NotFound>>
    {
        private readonly ActivityService _activityService;

        public DeleteMeetingEndpoint(ActivityService activityService)
        {
            _activityService = activityService;
        }

        public override void Configure()
        {
            Delete("/activity/meetings/{id:guid}");
            Summary(s =>
            {
                s.Summary = "Deletes an existing meeting activity.";
                s.Description = "Removes a meeting record identified by its unique ID.";

                s.Params["id"] = "The unique identifier (GUID) of the meeting to delete.";

                s.AddDeleteResponses("Meeting");
            });
        }

        public override async Task<Results<NoContent, NotFound>> ExecuteAsync(CancellationToken ct)
        {
            var id = Route<Guid>("id");

            var isDeleted = await _activityService.DeleteMeetingAsync(id, ct);
            if (isDeleted)
            {
                return TypedResults.NoContent();
            }

            return TypedResults.NotFound();
        }
    }
}
