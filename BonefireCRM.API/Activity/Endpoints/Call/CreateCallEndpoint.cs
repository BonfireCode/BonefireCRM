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

        public CreateCallEndpoint(ActivityService activityService)
        {
            _activityService = activityService;
        }

        public override void Configure()
        {
            Post("/activity/calls");
        }

        public override async Task<Results<Created<CreateCallResponse>, InternalServerError>> ExecuteAsync(CreateCallRequest request, CancellationToken ct)
        {
            var registerId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) !);
            var dtoCall = RequestToDtoMapper.MapToDto(request, registerId);

            var result = await _activityService.CreateCallAsync(dtoCall, ct);

            var response = result.Match<Results<Created<CreateCallResponse>, InternalServerError>>(
                createdCall => TypedResults.Created($"/activity/calls/{createdCall.Id}", createdCall.MapToResponse()),
                _ => TypedResults.InternalServerError());

            return response;
        }
    }
}
