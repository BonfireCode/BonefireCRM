// <copyright file="CreateTaskEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contrat.Call;
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
            throw new NotImplementedException();
        }
    }
} 
