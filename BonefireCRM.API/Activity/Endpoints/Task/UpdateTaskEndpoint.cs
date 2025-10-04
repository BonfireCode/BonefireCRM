// <copyright file="UpdateTaskEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contrat.Call;
using BonefireCRM.API.Contrat.Task;
using BonefireCRM.Domain.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BonefireCRM.API.Activity.Endpoints.Task
{
    public class UpdateTaskEndpoint : Endpoint<UpdateTaskRequest, Results<Created<UpdateTaskResponse>, InternalServerError>>
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

        public override async Task<Results<Created<UpdateTaskResponse>, InternalServerError>> ExecuteAsync(UpdateTaskRequest request, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
