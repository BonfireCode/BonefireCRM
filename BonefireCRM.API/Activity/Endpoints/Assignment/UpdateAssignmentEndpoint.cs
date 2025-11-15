// <copyright file="UpdateAssignmentEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Activity.Mappers.Assignment;
using BonefireCRM.API.Activity.Mappers.Task;
using BonefireCRM.API.Contrat.Activity.Assignment;
using BonefireCRM.API.Extensions;
using BonefireCRM.Domain.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BonefireCRM.API.Activity.Endpoints.Assignment
{
    public class UpdateAssignmentEndpoint : Endpoint<UpdateAssignmentRequest, Results<Ok<UpdateAssignmentResponse>, NotFound, InternalServerError>>
    {
        private readonly ActivityService _activityService;

        public UpdateAssignmentEndpoint(ActivityService activityService)
        {
            _activityService = activityService;
        }

        public override void Configure()
        {
            Put("/activity/assignments/{id:guid}");

            Summary(s =>
            {
                s.Summary = "Updates an existing assignment activity.";
                s.Description = "Updates the details of an existing assignment identified by its unique GUID.";
                s.Params["id"] = "The unique identifier (GUID) of the assignment to update.";
                s.AddGetResponses<GetAssignmentResponse>("Assignment");
            });
        }

        public override async Task<Results<Ok<UpdateAssignmentResponse>, NotFound, InternalServerError>> ExecuteAsync(UpdateAssignmentRequest request, CancellationToken ct)
        {
            var id = Route<Guid>("id");

            var dtoAssignment = request.MapToDto(id);

            var result = await _activityService.UpdateAssignmentAsync(dtoAssignment, ct);

            var response = result.Match<Results<Ok<UpdateAssignmentResponse>, NotFound, InternalServerError>>(
                updatedAssignment => TypedResults.Ok(updatedAssignment.MapToResponse()),
                _ => TypedResults.InternalServerError());

            return response;
        }
    }
}
