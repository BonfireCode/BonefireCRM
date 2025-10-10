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
            Summary(s =>
            {
                s.Summary = "Creates a new meeting activity.";
                s.Description = "Creates a new meeting linked to a contact and optionally to a company or deal.";

                s.Params[nameof(CreateMeetingRequest.ContactId)] = "The unique identifier of the contact associated with the meeting.";
                s.Params[nameof(CreateMeetingRequest.CompanyId)] = "Optional. The unique identifier of the company linked to the meeting.";
                s.Params[nameof(CreateMeetingRequest.DealId)] = "Optional. The unique identifier of the deal linked to the meeting.";
                s.Params[nameof(CreateMeetingRequest.StartTime)] = "The start date and time of the meeting.";
                s.Params[nameof(CreateMeetingRequest.EndTime)] = "The end date and time of the meeting.";
                s.Params[nameof(CreateMeetingRequest.Subject)] = "The subject or title of the meeting.";
                s.Params[nameof(CreateMeetingRequest.Notes)] = "Additional notes or comments about the meeting.";


                s.Response<Created<CreateMeetingResponse>>(201, "Meeting successfully created.");
                s.Response<ProblemDetails>(400, "Invalid request data. Validation errors are returned in problem+json format.", "application/problem+json");
                s.Response<UnauthorizedHttpResult>(401, "User is not authorized to perform this action.");
                s.Response<InternalServerError>(500, "An internal server error occurred while creating the meeting.");
            });
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
