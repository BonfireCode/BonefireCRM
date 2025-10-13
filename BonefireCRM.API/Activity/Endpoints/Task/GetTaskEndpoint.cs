// <copyright file="GetTaskEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Activity.Mappers.Task;
using BonefireCRM.API.Contrat.Task;
using BonefireCRM.API.Extensions;
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

                s.AddGetResponses<GetTaskResponse>("Task");
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
