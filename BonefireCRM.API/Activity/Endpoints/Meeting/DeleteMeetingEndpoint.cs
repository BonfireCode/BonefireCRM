// <copyright file="DeleteMeetingEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

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

                s.Response<NoContent>(204, "Meeting successfully deleted.");
                s.Response<NotFound>(404, "The specified meeting could not be found.");
                s.Response<ProblemDetails>(400, "Invalid request. The provided meeting ID is not valid.");
                s.Response<UnauthorizedHttpResult>(401, "User is not authorized to perform this action.");
                s.Response<InternalServerError>(500, "An internal server error occurred while deleting the meeting.");
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
