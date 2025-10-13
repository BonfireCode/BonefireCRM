// <copyright file="CreateCallEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Activity.Mappers.Call;
using BonefireCRM.API.Contrat.Call;
using BonefireCRM.API.Extensions;
using BonefireCRM.Domain.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Security.Claims;

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

                s.AddParamsFrom<CreateCallRequest>();
                s.AddCreateResponses<CreateCallResponse>("Call");
            });
        }

        public override async Task<Results<Created<CreateCallResponse>, InternalServerError>> ExecuteAsync(CreateCallRequest request, CancellationToken ct)
        {
            var registerId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var userId = await _userService.GetUserIdAsync(registerId, ct);

            var dtoCall = request.MapToDto(userId);

            var result = await _activityService.CreateCallAsync(dtoCall, ct);

            var response = result.Match<Results<Created<CreateCallResponse>, InternalServerError>>(
                createdCall => TypedResults.Created($"/activity/calls/{createdCall.Id}", createdCall.MapToResponse()),
                _ => TypedResults.InternalServerError());

            return response;
        }
    }
}
