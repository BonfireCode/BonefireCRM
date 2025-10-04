// <copyright file="UpdateMeetingEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contrat.Meeting;
using BonefireCRM.Domain.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BonefireCRM.API.Activity.Endpoints.Meeting
{
    public class UpdateMeetingEndpoint : Endpoint<UpdateMeetingRequest, Results<Created<UpdateMeetingResponse>, InternalServerError>>
    {
        private readonly ActivityService _activityService;

        public UpdateMeetingEndpoint(ActivityService activityService)
        {
            _activityService = activityService;
        }

        public override void Configure()
        {
            Put("/activity/mettings/{id:guid}");
        }

        public override async Task<Results<Created<UpdateMeetingResponse>, InternalServerError>> ExecuteAsync(UpdateMeetingRequest request, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
