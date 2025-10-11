// <copyright file="GetTaskEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Activity.Mappers.Task;
using BonefireCRM.API.Contrat.Task;
using BonefireCRM.Domain.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BonefireCRM.API.Activity.Endpoints.Task
{
    public class GetTaskEndpoint : EndpointWithoutRequest<Results<Ok<GetTaskResponse>, NotFound>>
    {
        private readonly ActivityService _activityService;

        public GetTaskEndpoint(ActivityService activityService)
        {
            _activityService = activityService;
        }

        public override void Configure()
        {
            Get("/activity/tasks/{id:guid}");

            Summary(s =>
            {
                s.Summary = "Retrieves a specific task activity by ID.";
                s.Description = "Fetches detailed information about a task identified by its unique GUID, including contact, company, and deal associations.";

                s.Params["id"] = "The unique identifier (GUID) of the task to retrieve.";

                s.Response<Ok<GetTaskResponse>>(200, "Task successfully retrieved.");
                s.Response<NotFound>(404, "The specified task could not be found.");
                s.Response<ProblemDetails>(400, "Invalid request data. Validation errors are returned in problem+json format.", "application/problem+json");
                s.Response<UnauthorizedHttpResult>(401, "User is not authorized to perform this action.");
                s.Response<InternalServerError>(500, "An internal server error occurred while retrieving the task.");
            });
        }

        public override async Task<Results<Ok<GetTaskResponse>, NotFound>> ExecuteAsync(CancellationToken ct)
        {
            var id = Route<Guid>("id");

            var result = await _activityService.GetTaskAsync(id, ct);

            var response = result.Match<Results<Ok<GetTaskResponse>, NotFound>>(
                dtoTask => TypedResults.Ok(dtoTask.MapToResponse()),
                TypedResults.NotFound());

            return response;
        }
    }
}
