// <copyright file="GetMeetingEndpoint.cs" company="Bonefire">
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
    public class GetMeetingEndpoint : EndpointWithoutRequest<Results<Ok<GetMeetingResponse>, NotFound>>
    {
        private readonly ActivityService _activityService;

        public GetMeetingEndpoint(ActivityService activityService)
        {
            _activityService = activityService;
        }

        public override void Configure()
        {
            Get("/activity/meetings/{id:guid}");

            Summary(s =>
            {
                s.Summary = "Retrieves a meeting activity by its ID.";
                s.Description = "Fetches detailed information about a specific meeting identified by its unique GUID.";
                s.Params["id"] = "The unique identifier (GUID) of the meeting to retrieve.";
                s.AddGetResponses<GetMeetingResponse>("Meeting");
            });
        }

        public override async Task<Results<Ok<GetMeetingResponse>, NotFound>> ExecuteAsync(CancellationToken ct)
        {
            var id = Route<Guid>("id");

            var result = await _activityService.GetMeetingAsync(id, ct);

            var response = result.Match<Results<Ok<GetMeetingResponse>, NotFound>>(
                dtoMeeting => TypedResults.Ok(dtoMeeting.MapToResponse()),
                TypedResults.NotFound());

            return response;
        }
    }
}
