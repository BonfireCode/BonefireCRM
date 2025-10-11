// <copyright file="UpdateMeetingEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Activity.Mappers.Meeting;
using BonefireCRM.API.Contrat.Meeting;
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
                s.Params[nameof(UpdateMeetingRequest.ContactId)] = "Unique identifier of the contact related to the meeting.";
                s.Params[nameof(UpdateMeetingRequest.CompanyId)] = "Unique identifier of the company associated with the meeting, if applicable.";
                s.Params[nameof(UpdateMeetingRequest.DealId)] = "Unique identifier of the deal associated with the meeting, if applicable.";
                s.Params[nameof(UpdateMeetingRequest.StartTime)] = "Date and time when the meeting starts.";
                s.Params[nameof(UpdateMeetingRequest.EndTime)] = "Date and time when the meeting ends.";
                s.Params[nameof(UpdateMeetingRequest.Subject)] = "Subject or title of the meeting.";
                s.Params[nameof(UpdateMeetingRequest.Notes)] = "Additional notes or comments about the meeting.";

                s.Response<Ok<UpdateMeetingResponse>>(200, "Meeting successfully updated.");
                s.Response<ProblemDetails>(400, "Invalid request data. Please verify the meeting ID and request body.", "application/problem+json");
                s.Response<UnauthorizedHttpResult>(401, "User is not authorized to update this meeting.");
                s.Response<NotFound>(404, "The specified meeting could not be found.");
                s.Response<InternalServerError>(500, "An error occurred while updating the meeting.");
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
