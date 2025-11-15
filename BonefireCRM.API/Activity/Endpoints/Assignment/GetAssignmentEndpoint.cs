// <copyright file="GetAssignmentEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Activity.Mappers.Task;
using BonefireCRM.API.Contrat.Activity.Assignment;
using BonefireCRM.API.Extensions;
using BonefireCRM.Domain.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BonefireCRM.API.Activity.Endpoints.Assignment
{
    public class GetAssignmentEndpoint : EndpointWithoutRequest<Results<Ok<GetAssignmentResponse>, NotFound>>
    {
        private readonly ActivityService _activityService;

        public GetAssignmentEndpoint(ActivityService activityService)
        {
            _activityService = activityService;
        }

        public override void Configure()
        {
            Get("/activity/assignments/{id:guid}");

            Summary(s =>
            {
                s.Summary = "Retrieves a specific assignment activity by ID.";
                s.Description = "Fetches detailed information about a assignment identified by its unique GUID, including contact, company, and deal associations.";
                s.Params["id"] = "The unique identifier (GUID) of the assignment to retrieve.";
                s.AddGetResponses<GetAssignmentResponse>("Assignment");
            });
        }

        public override async Task<Results<Ok<GetAssignmentResponse>, NotFound>> ExecuteAsync(CancellationToken ct)
        {
            var id = Route<Guid>("id");

            var result = await _activityService.GetAssignmentAsync(id, ct);

            var response = result.Match<Results<Ok<GetAssignmentResponse>, NotFound>>(
                dtoAssignment => TypedResults.Ok(dtoAssignment.MapToResponse()),
                TypedResults.NotFound());

            return response;
        }
    }
}
