// <copyright file="GetPipelineEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contrat.Pipeline;
using BonefireCRM.API.Extensions;
using BonefireCRM.API.Pipeline.Mappers;
using BonefireCRM.Domain.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BonefireCRM.API.Pipeline.Endpoints
{
    public class GetPipelineEndpoint : EndpointWithoutRequest<Results<Ok<GetPipelineResponse>, NotFound>>
    {
        private readonly PipelineService _pipelineService;

        public GetPipelineEndpoint(PipelineService pipelineService)
        {
            _pipelineService = pipelineService;
        }

        public override void Configure()
        {
            Get("/pipelines/{id:guid}");

            Summary(s =>
            {
                s.Summary = "Retrieves a specific pipeline by ID.";
                s.Description = "Fetches detailed information about a pipeline identified by its unique ID.";

                s.Params["id"] = "The unique identifier (GUID) of the pipeline to retrieve.";

                s.AddGetResponses<GetPipelineResponse>("Pipeline");
            });
        }

        public override async Task<Results<Ok<GetPipelineResponse>, NotFound>> ExecuteAsync(CancellationToken ct)
        {
            var id = Route<Guid>("id");

            var result = await _pipelineService.GetPipelineAsync(id, ct);

            var response = result.Match<Results<Ok<GetPipelineResponse>, NotFound>>(
                dtoPipeline => TypedResults.Ok(dtoPipeline.MapToResponse()),
                TypedResults.NotFound());

            return response;
        }
    }
}
