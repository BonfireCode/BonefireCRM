// <copyright file="UpdateCallEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Company.Mappers.Call;
using BonefireCRM.API.Contrat.Call;
using BonefireCRM.Domain.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BonefireCRM.API.Activity.Endpoints.Call
{
    public class UpdateCallEndpoint : Endpoint<UpdateCallRequest, Results<Ok<UpdateCallResponse>, NotFound, InternalServerError>>
    {
        private readonly ActivityService _activityService;

        public UpdateCallEndpoint(ActivityService activityService)
        {
            _activityService = activityService;
        }

        public override void Configure()
        {
            Put("/activity/calls/{id:guid}");
            Summary(s =>
            {
                s.Summary = "Updates an existing call activity.";
                s.Description = "Updates the details of an existing call identified by its unique ID.";

                s.Params["id"] = "The unique identifier (GUID) of the call to update.";
                s.Params[nameof(UpdateCallRequest.ContactId)] = "The unique identifier of the contact associated with the call.";
                s.Params[nameof(UpdateCallRequest.CompanyId)] = "Optional. The unique identifier of the company linked to the call.";
                s.Params[nameof(UpdateCallRequest.DealId)] = "Optional. The unique identifier of the deal linked to the call.";
                s.Params[nameof(UpdateCallRequest.CallTime)] = "The date and time when the call occurred.";
                s.Params[nameof(UpdateCallRequest.Duration)] = "The duration of the call.";
                s.Params[nameof(UpdateCallRequest.Notes)] = "Additional notes or comments about the call.";

                s.Response<Ok<UpdateCallResponse>>(200, "Call successfully updated.");
                s.Response<NotFound>(404, "The specified call could not be found.");
                s.Response<ProblemDetails>(400, "Invalid request data. Validation errors are returned in problem+json format.", "application/problem+json");
                s.Response<UnauthorizedHttpResult>(401, "User is not authorized to perform this action.");
                s.Response<InternalServerError>(500, "An internal server error occurred while updating the call.");
            });
        }

        public override async Task<Results<Ok<UpdateCallResponse>, NotFound, InternalServerError>> ExecuteAsync(UpdateCallRequest request, CancellationToken ct)
        {
            var id = Route<Guid>("id");

            var dtoCall = RequestToDtoMapper.MapToDto(request, id);

            var result = await _activityService.UpdateCallAsync(dtoCall, ct);

            var response = result.Match<Results<Ok<UpdateCallResponse>, NotFound, InternalServerError>>(
                updatedCall => TypedResults.Ok(updatedCall.MapToResponse()),
                _ => TypedResults.InternalServerError());

            return response;
        }
    }
}
