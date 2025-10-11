// <copyright file="DeleteCallEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.Domain.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BonefireCRM.API.Activity.Endpoints.Call
{
    public class DeleteCallEndpoint : EndpointWithoutRequest<Results<NoContent, NotFound>>
    {
        private readonly ActivityService _activityService;

        public DeleteCallEndpoint(ActivityService activityService)
        {
            _activityService = activityService;
        }

        public override void Configure()
        {
            Delete("/activity/calls/{id:guid}");
            Summary(s =>
            {
                s.Summary = "Deletes an existing call activity.";
                s.Description = "Removes a call record identified by its unique ID. Returns 204 if deleted successfully, or 404 if the call was not found.";

                s.Params["id"] = "The unique identifier (GUID) of the call to delete.";

                s.Response<NoContent>(204, "Call successfully deleted.");
                s.Response<NotFound>(404, "The specified call could not be found.");
                s.Response<ProblemDetails>(400, "Invalid request. The provided call ID is not valid.");
                s.Response<UnauthorizedHttpResult>(401, "User is not authorized to perform this action.");
                s.Response<InternalServerError>(500, "An internal server error occurred while deleting the call.");
            });
        }

        public override async Task<Results<NoContent, NotFound>> ExecuteAsync(CancellationToken ct)
        {
            var id = Route<Guid>("id");

            var isDeleted = await _activityService.DeleteCallAsync(id, ct);
            if (isDeleted)
            {
                return TypedResults.NoContent();
            }

            return TypedResults.NotFound();
        }
    }
}
