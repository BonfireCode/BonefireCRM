// <copyright file="CreateCallEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using System.Security.Claims;
using BonefireCRM.API.Company.Mappers.Call;
using BonefireCRM.API.Contrat.Call;
using BonefireCRM.Domain.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BonefireCRM.API.Activity.Endpoints.Call
{
    public class CreateCallEndpoint : Endpoint<CreateCallRequest, Results<Created<CreateCallResponse>, InternalServerError>>
    {
        private readonly ActivityService _activityService;
        private readonly UserService _userService;

        public CreateCallEndpoint(ActivityService activityService, UserService userService)
        {
            _activityService = activityService;
            _userService = userService;
        }

        public override void Configure()
        {
            Post("/activity/calls");
            Summary(s =>
            {
                s.Summary = "Creates a new call activity.";
                s.Description = "Creates a new call linked to a contact and optionally to a company or deal.";

                s.Params[nameof(CreateCallRequest.ContactId)] = "The unique identifier of the contact associated with the call.";
                s.Params[nameof(CreateCallRequest.CompanyId)] = "Optional. The unique identifier of the company linked to the call.";
                s.Params[nameof(CreateCallRequest.DealId)] = "Optional. The unique identifier of the deal linked to the call.";
                s.Params[nameof(CreateCallRequest.CallTime)] = "The date and time when the call occurred.";
                s.Params[nameof(CreateCallRequest.Duration)] = "The duration of the call.";
                s.Params[nameof(CreateCallRequest.Notes)] = "Additional notes or comments about the call.";

                s.Response<Created<CreateCallResponse>>(201, "Call successfully created.");
                s.Response<ProblemDetails>(400, "Invalid request data. Returns validation problem details.", "application/problem+json");
                s.Response<UnauthorizedHttpResult>(401, "User is not authorized to perform this action.");
                s.Response<InternalServerError>(500, "An internal server error occurred while creating the call.");
            });
        }

        public override async Task<Results<Created<CreateCallResponse>, InternalServerError>> ExecuteAsync(CreateCallRequest request, CancellationToken ct)
        {
            var registerId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var userId = await _userService.GetUserIdAsync(registerId, ct);

            var dtoCall = RequestToDtoMapper.MapToDto(request, userId);

            var result = await _activityService.CreateCallAsync(dtoCall, ct);

            var response = result.Match<Results<Created<CreateCallResponse>, InternalServerError>>(
                createdCall => TypedResults.Created($"/activity/calls/{createdCall.Id}", createdCall.MapToResponse()),
                _ => TypedResults.InternalServerError());

            return response;
        }
    }
}
