// <copyright file="GetPipelineStageEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contrat.PipelineStage;
using BonefireCRM.API.Extensions;
using BonefireCRM.API.PipelineStage.Mappers;
using BonefireCRM.Domain.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BonefireCRM.API.PipelineStage.Endpoints
{
    public class GetPipelineStageEndpoint : EndpointWithoutRequest<Results<Ok<GetPipelineStageResponse>, NotFound>>
    {
        private readonly PipelineStageService _pipelineStageService;

        public GetPipelineStageEndpoint(PipelineStageService pipelineStageService)
        {
            _pipelineStageService = pipelineStageService;
        }

        public override void Configure()
        {
            Get("/pipelinestages/{id:guid}");

            Summary(s =>
            {
                s.Summary = "Retrieves a specific pipeline stage by ID.";
                s.Description = "Fetches detailed information about a pipeline stage identified by its unique ID.";

                s.Params["id"] = "The unique identifier (GUID) of the pipeline stage to retrieve.";

                s.AddGetResponses<GetPipelineStageResponse>("Pipeline Stage");
            });
        }

        public override async Task<Results<Ok<GetPipelineStageResponse>, NotFound>> ExecuteAsync(CancellationToken ct)
        {
            var id = Route<Guid>("id");
            var result = await _pipelineStageService.GetPipelineStageAsync(id, ct);

            var response = result.Match<Results<Ok<GetPipelineStageResponse>, NotFound>>(
                dtoPipelineStage => TypedResults.Ok(dtoPipelineStage.MapToResponse()),
                TypedResults.NotFound());

            return response;
        }
    }
}
