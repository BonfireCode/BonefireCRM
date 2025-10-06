// <copyright file="UpdateTaskEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Company.Mappers.Task;
using BonefireCRM.API.Contrat.Task;
using BonefireCRM.Domain.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BonefireCRM.API.Activity.Endpoints.Task
{
    public class UpdateTaskEndpoint : Endpoint<UpdateTaskRequest, Results<Ok<UpdateTaskResponse>, NotFound, InternalServerError>>
    {
        private readonly ActivityService _activityService;

        public UpdateTaskEndpoint(ActivityService activityService)
        {
            _activityService = activityService;
        }

        public override void Configure()
        {
            Put("/activity/tasks/{id:guid}");
        }

        public override async Task<Results<Ok<UpdateTaskResponse>, NotFound, InternalServerError>> ExecuteAsync(UpdateTaskRequest request, CancellationToken ct)
        {
            var id = Route<Guid>("id");

            var dtoTask = RequestToDtoMapper.MapToDto(request, id);

            var result = await _activityService.UpdateTaskAsync(dtoTask, ct);

            var response = result.Match<Results<Ok<UpdateTaskResponse>, NotFound, InternalServerError>>(
                updatedTask => TypedResults.Ok(updatedTask.MapToResponse()),
                _ => TypedResults.InternalServerError());

            return response;
        }
    }
}
