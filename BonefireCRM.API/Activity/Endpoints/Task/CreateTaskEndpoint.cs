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

                s.Params[nameof(CreateTaskRequest.ContactId)] = "The unique identifier of the contact associated with the task.";
                s.Params[nameof(CreateTaskRequest.CompanyId)] = "Optional. The unique identifier of the company linked to the task.";
                s.Params[nameof(CreateTaskRequest.DealId)] = "Optional. The unique identifier of the deal linked to the task.";
                s.Params[nameof(CreateTaskRequest.Subject)] = "The subject or title of the task.";
                s.Params[nameof(CreateTaskRequest.Description)] = "A detailed description of the task.";
                s.Params[nameof(CreateTaskRequest.DueDate)] = "The due date and time for completing the task.";
                s.Params[nameof(CreateTaskRequest.IsCompleted)] = "Indicates whether the task has been completed.";

                s.Response<Created<CreateTaskResponse>>(201, "Task successfully created.");
                s.Response<ProblemDetails>(400, "Invalid request data. Validation errors are returned in problem+json format.", "application/problem+json");
                s.Response<UnauthorizedHttpResult>(401, "User is not authorized to perform this action.");
                s.Response<InternalServerError>(500, "An internal server error occurred while creating the task.");
            });
        }

        public override async Task<Results<Created<CreateTaskResponse>, InternalServerError>> ExecuteAsync(CreateTaskRequest request, CancellationToken ct)
        {
            var registerId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) !);
            var userId = await _userService.GetUserIdAsync(registerId, ct);

            var dtoTask = RequestToDtoMapper.MapToDto(request, userId);

            var result = await _activityService.CreateTaskAsync(dtoTask, ct);

            var response = result.Match<Results<Created<CreateTaskResponse>, InternalServerError>>(
                createdTask => TypedResults.Created($"/activity/tasks/{createdTask.Id}", createdTask.MapToResponse()),
                _ => TypedResults.InternalServerError());

            return response;
        }
    }
}
