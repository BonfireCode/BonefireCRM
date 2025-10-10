// <copyright file="CreateMeetingEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Company.Mappers.Meeting;
using BonefireCRM.API.Contrat.Meeting;
using BonefireCRM.API.Contrat.Meeting;
using BonefireCRM.Domain.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Security.Claims;

namespace BonefireCRM.API.Activity.Endpoints.Meeting
{
    public class CreateMeetingEndpoint : Endpoint<CreateMeetingRequest, Results<Created<CreateMeetingResponse>, InternalServerError>>
    {
        private readonly ActivityService _activityService;
        private readonly UserService _userService;

        public CreateMeetingEndpoint(ActivityService activityService, UserService userService)
        {
            _activityService = activityService;
            _userService = userService;
        }

        public override void Configure()
        {
            Post("/activity/meetings");
        }

        public override async Task<Results<Created<CreateMeetingResponse>, InternalServerError>> ExecuteAsync(CreateMeetingRequest request, CancellationToken ct)
        {
            var registerId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) !);
            var userId = await _userService.GetUserIdAsync(registerId, ct);

            var dtoMeeting = RequestToDtoMapper.MapToDto(request, userId);

            var result = await _activityService.CreateMeetingAsync(dtoMeeting, ct);

            var response = result.Match<Results<Created<CreateMeetingResponse>, InternalServerError>>(
                createdMeeting => TypedResults.Created($"/activity/meetings{createdMeeting.Id}", createdMeeting.MapToResponse()),
                _ => TypedResults.InternalServerError());

            return response;
        }
    }
}
