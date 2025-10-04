// <copyright file="GetTaskEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contrat.Call;
using BonefireCRM.API.Contrat.Task;
using BonefireCRM.Domain.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BonefireCRM.API.Activity.Endpoints.Task
{
    public class GetTaskEndpoint : EndpointWithoutRequest<Results<Ok<GetTaskResponse>, NotFound>>
    {
        private readonly ActivityService _activityService;

        public GetTaskEndpoint(ActivityService activityService)
        {
            _activityService = activityService;
        }

        public override void Configure()
        {
            Get("/activity/tasks/{id:guid}");
        }

        public override async Task<Results<Ok<GetTaskResponse>, NotFound>> ExecuteAsync(CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
