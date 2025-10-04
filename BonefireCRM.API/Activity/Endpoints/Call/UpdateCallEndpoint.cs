// <copyright file="UpdateCallEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contrat.Call;
using BonefireCRM.Domain.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BonefireCRM.API.Activity.Endpoints.Call
{
    public class UpdateCallEndpoint : Endpoint<UpdateCallRequest, Results<Created<UpdateCallResponse>, InternalServerError>>
    {
        private readonly ActivityService _activityService;

        public UpdateCallEndpoint(ActivityService activityService)
        {
            _activityService = activityService;
        }

        public override void Configure()
        {
            Put("/activity/calls/{id:guid}");
        }

        public override async Task<Results<Created<UpdateCallResponse>, InternalServerError>> ExecuteAsync(UpdateCallRequest request, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
