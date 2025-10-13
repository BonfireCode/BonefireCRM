﻿// <copyright file="UpdateMeetingEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Activity.Mappers.Meeting;
using BonefireCRM.API.Contrat.Meeting;
using BonefireCRM.API.Extensions;
using BonefireCRM.Domain.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BonefireCRM.API.Activity.Endpoints.Meeting
{
    public class UpdateMeetingEndpoint : Endpoint<UpdateMeetingRequest, Results<Ok<UpdateMeetingResponse>, NotFound, InternalServerError>>
    {
        private readonly ActivityService _activityService;

        public UpdateMeetingEndpoint(ActivityService activityService)
        {
            _activityService = activityService;
        }

        public override void Configure()
        {
            Put("/activity/mettings/{id:guid}");

            Summary(s =>
            {
                s.Summary = "Updates an existing meeting activity.";
                s.Description = "Updates the details of a meeting identified by its unique GUID. The request includes information such as contact, company, deal, meeting times, subject, and notes.";

                s.Params["id"] = "Unique identifier (GUID) of the meeting to update.";

                s.AddParamsFrom<UpdateMeetingRequest>();
                s.AddUpdateResponses<UpdateMeetingResponse>("Call");
            });
        }

        public override async Task<Results<Ok<UpdateMeetingResponse>, NotFound, InternalServerError>> ExecuteAsync(UpdateMeetingRequest request, CancellationToken ct)
        {
            var id = Route<Guid>("id");

            var dtoMeeting = request.MapToDto(id);

            var result = await _activityService.UpdateMeetingAsync(dtoMeeting, ct);

            var response = result.Match<Results<Ok<UpdateMeetingResponse>, NotFound, InternalServerError>>(
                updatedMeeting => TypedResults.Ok(updatedMeeting.MapToResponse()),
                _ => TypedResults.InternalServerError());

            return response;
        }
    }
}
