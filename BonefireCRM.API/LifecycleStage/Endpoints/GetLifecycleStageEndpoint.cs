// <copyright file="GetLifecycleStageEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contrat.LifeCycleStage;
using BonefireCRM.API.Extensions;
using BonefireCRM.API.LifecycleStage.Mappers;
using BonefireCRM.Domain.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BonefireCRM.API.LifecycleStage.Endpoints
{
    public class GetLifecycleStageEndpoint : EndpointWithoutRequest<Results<Ok<GetLifecycleStageResponse>, NotFound>>
    {
        private readonly LifecycleStageService _lifecycleStageService;

        public GetLifecycleStageEndpoint(LifecycleStageService lifecycleStageService)
        {
            _lifecycleStageService = lifecycleStageService;
        }

        public override void Configure()
        {
            Get("/lifecyclestages/{id:guid}");

            Summary(s =>
            {
                s.Summary = "Retrieves a specific lifecycle stage by ID.";
                s.Description = "Fetches detailed information about a lifecycle stage identified by its unique ID.";

                s.Params["id"] = "The unique identifier (GUID) of the lifecycle stage to retrieve.";

                s.AddGetResponses<GetLifecycleStageResponse>("Lifecycle Stage");
            });
        }

        public override async Task<Results<Ok<GetLifecycleStageResponse>, NotFound>> ExecuteAsync(CancellationToken ct)
        {
            var id = Route<Guid>("id");
            var result = await _lifecycleStageService.GetLifecycleStageAsync(id, ct);

            var response = result.Match<Results<Ok<GetLifecycleStageResponse>, NotFound>>(
                dtoLifecycleStage => TypedResults.Ok(dtoLifecycleStage.MapToResponse()),
                TypedResults.NotFound());

            return response;
        }
    }
}
