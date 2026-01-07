// <copyright file="GetAllAssignmentsEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Activity.Mappers.Assignment;
using BonefireCRM.API.Activity.Mappers.Task;
using BonefireCRM.API.Contrat.Activity.Assignment;
using BonefireCRM.API.Extensions;
using BonefireCRM.Domain.Services;
using FastEndpoints;

namespace BonefireCRM.API.Activity.Endpoints.Assignment
{
    public class GetAllAssignmentsEndpoint : Endpoint<GetAssignmentsRequest, IEnumerable<GetAssignmentResponse>>
    {
        private readonly ActivityService _activityService;

        public GetAllAssignmentsEndpoint(ActivityService activityService)
        {
            _activityService = activityService;
        }

        public override void Configure()
        {
            Get("/activity/assignments");

            Summary(s =>
            {
                s.Summary = "Retrieves all specific assignments";
                s.Description = "Fetches detailed information about assignments";

                s.AddGetAllResponses<IEnumerable<GetAssignmentResponse>>("Assignments");
            });
        }

        public override async Task<IEnumerable<GetAssignmentResponse>> ExecuteAsync(GetAssignmentsRequest request, CancellationToken ct)
        {
            var dtoTasks = request.MapToDto();

            var result = _activityService.GetAllAssignments(dtoTasks, ct);

            var response = result.Select(assignment => assignment.MapToResponse());

            return await Task.Run(() => response);
        }
    }
}