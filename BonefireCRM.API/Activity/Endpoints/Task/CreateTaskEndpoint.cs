// <copyright file="CreateTaskEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>
using System.Security.Claims;
using BonefireCRM.API.Activity.Mappers.Task;
using BonefireCRM.API.Contrat.Task;
using BonefireCRM.API.Extensions;
using BonefireCRM.Domain.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BonefireCRM.API.Activity.Endpoints.Task
{
    public class CreateTaskEndpoint : Endpoint<CreateTaskRequest, Results<Created<CreateTaskResponse>, InternalServerError>>
    {
        private readonly ActivityService _activityService;
        private readonly UserService _userService;

        public CreateTaskEndpoint(ActivityService activityService, UserService userService)
        {
            _activityService = activityService;
            _userService = userService;
        }

        public override void Configure()
        {
            Post("/activity/tasks");

            Summary(s =>
            {
                s.Summary = "Creates a new task activity.";
                s.Description = "Creates a new task linked to a contact and optionally to a company or deal.";

                s.AddParamsFrom<CreateTaskRequest>();
                s.AddCreateResponses<CreateTaskResponse>("Task");
            });
        }

        public override async Task<Results<Created<CreateTaskResponse>, InternalServerError>> ExecuteAsync(CreateTaskRequest request, CancellationToken ct)
        {
            var registerId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) !);
            var userId = await _userService.GetUserIdAsync(registerId, ct);

            var dtoTask = request.MapToDto(userId);

            var result = await _activityService.CreateTaskAsync(dtoTask, ct);

            var response = result.Match<Results<Created<CreateTaskResponse>, InternalServerError>>(
                createdTask => TypedResults.Created($"/activity/tasks/{createdTask.Id}", createdTask.MapToResponse()),
                _ => TypedResults.InternalServerError());

            return response;
        }
    }
}
