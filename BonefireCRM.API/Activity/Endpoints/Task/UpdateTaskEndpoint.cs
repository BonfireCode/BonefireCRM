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

            Summary(s =>
            {
                s.Summary = "Updates an existing task activity.";
                s.Description = "Updates the details of an existing task identified by its unique GUID.";

                s.Params["id"] = "The unique identifier (GUID) of the task to update.";
                s.Params[nameof(UpdateTaskRequest.ContactId)] = "The unique identifier of the contact associated with the task.";
                s.Params[nameof(UpdateTaskRequest.CompanyId)] = "Optional. The unique identifier of the company linked to the task.";
                s.Params[nameof(UpdateTaskRequest.DealId)] = "Optional. The unique identifier of the deal linked to the task.";
                s.Params[nameof(UpdateTaskRequest.Subject)] = "The subject or title of the task.";
                s.Params[nameof(UpdateTaskRequest.Description)] = "A detailed description of the task.";
                s.Params[nameof(UpdateTaskRequest.DueDate)] = "The due date and time for completing the task.";
                s.Params[nameof(UpdateTaskRequest.IsCompleted)] = "Indicates whether the task has been completed.";

                s.Response<Ok<UpdateTaskResponse>>(200, "Task successfully updated.");
                s.Response<NotFound>(404, "The specified task could not be found.");
                s.Response<ProblemDetails>(400, "Invalid request data. Validation errors are returned in problem+json format.", "application/problem+json");
                s.Response<UnauthorizedHttpResult>(401, "User is not authorized to perform this action.");
                s.Response<InternalServerError>(500, "An internal server error occurred while updating the task.");
            });
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
