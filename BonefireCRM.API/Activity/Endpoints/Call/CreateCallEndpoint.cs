// <copyright file="CreateCallEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contrat.Call;
using BonefireCRM.Domain.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BonefireCRM.API.Activity.Endpoints.Call
{
    public class CreateCallEndpoint : Endpoint<CreateCallRequest, Results<Created<CreateCallResponse>, InternalServerError>>
    {
        private readonly ActivityService _activityService;

        public CreateCallEndpoint(ActivityService activityService)
        {
            _activityService = activityService;
        }

        public override void Configure()
        {
            Post("/activity/calls");
        }

        public override async Task<Results<Created<CreateCallResponse>, InternalServerError>> ExecuteAsync(CreateCallRequest request, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
