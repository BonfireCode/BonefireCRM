// <copyright file="GetCallEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Activity.Mappers.Call;
using BonefireCRM.API.Contrat.Call;
using BonefireCRM.API.Extensions;
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

            Summary(s =>
            {
                s.Summary = "Retrieves a specific call activity by ID.";
                s.Description = "Fetches detailed information about a call identified by its unique ID.";

                s.Params["id"] = "The unique identifier (GUID) of the call to retrieve.";

                s.AddGetResponses<GetCallResponse>("Call");
            });
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
