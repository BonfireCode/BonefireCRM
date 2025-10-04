// <copyright file="CreateMeetingEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contrat.Meeting;
using BonefireCRM.Domain.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BonefireCRM.API.Activity.Endpoints.Meeting
{
    public class CreateMeetingEndpoint : Endpoint<CreateMeetingRequest, Results<Created<CreateMeetingResponse>, InternalServerError>>
    {
        private readonly ActivityService _activityService;

        public CreateMeetingEndpoint(ActivityService activityService)
        {
            _activityService = activityService;
        }

        public override void Configure()
        {
            Post("/activity/meetings");
        }

        public override async Task<Results<Created<CreateMeetingResponse>, InternalServerError>> ExecuteAsync(CreateMeetingRequest request, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
