// <copyright file="DeleteTaskEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.Domain.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BonefireCRM.API.Activity.Endpoints.Task
{
    public class DeleteTaskEndpoint : EndpointWithoutRequest<Results<NoContent, NotFound>>
    {
        private readonly ActivityService _activityService;

        public DeleteTaskEndpoint(ActivityService activityService)
        {
            _activityService = activityService;
        }

        public override void Configure()
        {
            Delete("/activity/tasks/{id:guid}");
            Summary(s =>
            {
                s.Summary = "Deletes an existing task activity.";
                s.Description = "Deletes a specific task identified by its unique GUID.";

                s.Params["id"] = "The unique identifier (GUID) of the task to delete.";

                s.Response<NoContent>(204, "Task successfully deleted.");
                s.Response<NotFound>(404, "The specified task could not be found.");
                s.Response<UnauthorizedHttpResult>(401, "User is not authorized to perform this action.");
                s.Response<ProblemDetails>(400, "Invalid request data. Validation errors are returned in problem+json format.", "application/problem+json");
                s.Response<InternalServerError>(500, "An internal server error occurred while deleting the task.");
            });
        }

        public override async Task<Results<NoContent, NotFound>> ExecuteAsync(CancellationToken ct)
        {
            var id = Route<Guid>("id");

            var isDeleted = await _activityService.DeleteTaskAsync(id, ct);
            if (isDeleted)
            {
                return TypedResults.NoContent();
            }

            return TypedResults.NotFound();
        }
    }
}
