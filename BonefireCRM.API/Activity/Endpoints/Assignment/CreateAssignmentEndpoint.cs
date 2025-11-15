// <copyright file="CreateAssignmentEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>
using System.Security.Claims;
using BonefireCRM.API.Activity.Mappers.Assignment;
using BonefireCRM.API.Activity.Mappers.Task;
using BonefireCRM.API.Contrat.Activity.Assignment;
using BonefireCRM.API.Extensions;
using BonefireCRM.Domain.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BonefireCRM.API.Activity.Endpoints.Assignment
{
    public class CreateAssignmentEndpoint : Endpoint<CreateAssignmentRequest, Results<Created<CreateAssignmentResponse>, InternalServerError>>
    {
        private readonly ActivityService _activityService;
        private readonly UserService _userService;

        public CreateAssignmentEndpoint(ActivityService activityService, UserService userService)
        {
            _activityService = activityService;
            _userService = userService;
        }

        public override void Configure()
        {
            Post("/activity/assignments");

            Summary(s =>
            {
                s.Summary = "Creates a new assignment activity.";
                s.Description = "Creates a new assignment linked to a contact and optionally to a company or deal.";
                s.AddCreateResponses<CreateAssignmentResponse>("Assignment");
            });
        }

        public override async Task<Results<Created<CreateAssignmentResponse>, InternalServerError>> ExecuteAsync(CreateAssignmentRequest request, CancellationToken ct)
        {
            var registerId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) !);
            var userId = await _userService.GetUserIdAsync(registerId, ct);

            var dtoAssignment = request.MapToDto(userId);

            var result = await _activityService.CreateAssignmentAsync(dtoAssignment, ct);

            var response = result.Match<Results<Created<CreateAssignmentResponse>, InternalServerError>>(
                createdAssignment => TypedResults.Created($"/activity/assignments/{createdAssignment.Id}", createdAssignment.MapToResponse()),
                _ => TypedResults.InternalServerError());

            return response;
        }
    }
}
