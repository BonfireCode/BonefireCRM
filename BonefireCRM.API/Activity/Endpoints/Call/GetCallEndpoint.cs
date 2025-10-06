// <copyright file="GetCallEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Company.Mappers.Call;
using BonefireCRM.API.Contrat.Call;
using BonefireCRM.Domain.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BonefireCRM.API.Activity.Endpoints.Call
{
    public class GetCallEndpoint : EndpointWithoutRequest<Results<Ok<GetCallResponse>, NotFound>>
    {
        private readonly ActivityService _activityService;

        public GetCallEndpoint(ActivityService activityService)
        {
            _activityService = activityService;
        }

        public override void Configure()
        {
            Get("/activity/calls/{id:guid}");
        }

        public override async Task<Results<Ok<GetCallResponse>, NotFound>> ExecuteAsync(CancellationToken ct)
        {
            var id = Route<Guid>("id");

            var result = await _activityService.GetCallAsync(id, ct);

            var response = result.Match<Results<Ok<GetCallResponse>, NotFound>>(
                dtoCall => TypedResults.Ok(dtoCall.MapToResponse()),
                TypedResults.NotFound());

            return response;
        }
    }
}
