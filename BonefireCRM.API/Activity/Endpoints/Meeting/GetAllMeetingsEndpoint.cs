// <copyright file="GetAllMeetingsEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Activity.Mappers.Meeting;
using BonefireCRM.API.Contrat.Meeting;
using BonefireCRM.API.Extensions;
using BonefireCRM.Domain.Services;
using FastEndpoints;

namespace BonefireCRM.API.Activity.Endpoints.Meeting
{
    public class GetAllMeetingsEndpoint : Endpoint<GetMeetingsRequest, IEnumerable<GetMeetingResponse>>
    {
        private readonly ActivityService _activityService;

        public GetAllMeetingsEndpoint(ActivityService activityService)
        {
            _activityService = activityService;
        }

        public override void Configure()
        {
            Get("/activity/meetings");

            Summary(s =>
            {
                s.Summary = "Retrieves all specific meetings";
                s.Description = "Fetches detailed information about meetings";

                s.AddGetAllResponses<IEnumerable<GetMeetingResponse>>("Meetings");
            });
        }

        public override async Task<IEnumerable<GetMeetingResponse>> ExecuteAsync(GetMeetingsRequest request, CancellationToken ct)
        {
            var dtoMeetings = request.MapToDto();

            var result = _activityService.GetAllMeetings(dtoMeetings, ct);

            var response = result.Select(m => m.MapToResponse());

            return await Task.Run(() => response);
        }
    }
}