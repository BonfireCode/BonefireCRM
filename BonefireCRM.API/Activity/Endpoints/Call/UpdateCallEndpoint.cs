// <copyright file="UpdateCallEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using System.Security.Claims;
using BonefireCRM.API.Activity.Mappers.Call;
using BonefireCRM.API.Contrat.Call;
using BonefireCRM.API.Extensions;
using BonefireCRM.Domain.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BonefireCRM.API.Activity.Endpoints.Call
{
    public class UpdateCallEndpoint : Endpoint<UpdateCallRequest, Results<Ok<UpdateCallResponse>, NotFound, InternalServerError>>
    {
        private readonly ActivityService _activityService;
        private readonly UserService _userService;

        public UpdateCallEndpoint(ActivityService activityService, UserService userService)
        {
            _activityService = activityService;
            _userService = userService;
        }

        public override void Configure()
        {
            Put("/activity/calls/{id:guid}");
            Summary(s =>
            {
                s.Summary = "Updates an existing call activity.";
                s.Description = "Updates the details of an existing call identified by its unique ID.";
                s.Params["id"] = "The unique identifier (GUID) of the call to update.";
                s.AddUpdateResponses<UpdateCallResponse>("Call");
            });
        }

        public override async Task<Results<Ok<UpdateCallResponse>, NotFound, InternalServerError>> ExecuteAsync(UpdateCallRequest request, CancellationToken ct)
        {
            var id = Route<Guid>("id");
            var registerId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var userId = await _userService.GetUserIdAsync(registerId, ct);

            var dtoCall = request.MapToDto(id, userId);

            var result = await _activityService.UpdateCallAsync(dtoCall, ct);

            var response = result.Match<Results<Ok<UpdateCallResponse>, NotFound, InternalServerError>>(
                updatedCall => TypedResults.Ok(updatedCall.MapToResponse()),
                _ => TypedResults.InternalServerError());

            return response;
        }
    }
}
