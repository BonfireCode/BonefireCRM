// <copyright file="CreateTaskEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>
using System.Security.Claims;
using BonefireCRM.API.Company.Mappers.Task;
using BonefireCRM.API.Contrat.Task;
using BonefireCRM.Domain.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BonefireCRM.API.Activity.Endpoints.Task
{
    public class CreateTaskEndpoint : Endpoint<CreateTaskRequest, Results<Created<CreateTaskResponse>, InternalServerError>>
    {
        private readonly ActivityService _activityService;

        public CreateTaskEndpoint(ActivityService activityService)
        {
            _activityService = activityService;
        }

        public override void Configure()
        {
            Post("/activity/tasks");
        }

        public override async Task<Results<Created<CreateTaskResponse>, InternalServerError>> ExecuteAsync(CreateTaskRequest request, CancellationToken ct)
        {
            var registerId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var dtoTask = RequestToDtoMapper.MapToDto(request, registerId);

            var result = await _activityService.CreateTaskAsync(dtoTask, ct);

            var response = result.Match<Results<Created<CreateTaskResponse>, InternalServerError>>(
                createdTask => TypedResults.Created($"/activity/tasks/{createdTask.Id}", createdTask.MapToResponse()),
                _ => TypedResults.InternalServerError());

            return response;
        }
    }
}
