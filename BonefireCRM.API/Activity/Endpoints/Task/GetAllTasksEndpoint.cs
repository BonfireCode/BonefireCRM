// <copyright file="GetAllTasksEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Activity.Mappers.Task;
using BonefireCRM.API.Contrat.Task;
using BonefireCRM.API.Extensions;
using BonefireCRM.Domain.Services;
using FastEndpoints;

namespace BonefireCRM.API.Activity.Endpoints.Task
{
    public class GetAllTasksEndpoint : Endpoint<GetTasksRequest, IEnumerable<GetTaskResponse>>
    {
        private readonly ActivityService _activityService;

        public GetAllTasksEndpoint(ActivityService activityService)
        {
            _activityService = activityService;
        }

        public override void Configure()
        {
            Get("/tasks");

            Summary(s =>
            {
                s.Summary = "Retrieves all specific tasks";
                s.Description = "Fetches detailed information about tasks";

                s.AddGetAllResponses<IEnumerable<GetTaskResponse>>("Tasks");
            });
        }

        public override async Task<IEnumerable<GetTaskResponse>> ExecuteAsync(GetTasksRequest request, CancellationToken ct)
        {
            var dtoTasks = request.MapToDto();

            var result = _activityService.GetAllTasks(dtoTasks, ct);

            var response = result.Select(t => t.MapToResponse());

            return await System.Threading.Tasks.Task.Run(() => response);
        }
    }
}