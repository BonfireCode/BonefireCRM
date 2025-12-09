// <copyright file="GetAllPipelinesEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contrat.Pipeline;
using BonefireCRM.API.Extensions;
using BonefireCRM.API.Pipeline.Mappers;
using BonefireCRM.Domain.Services;
using FastEndpoints;

namespace BonefireCRM.API.Pipeline.Endpoints
{
    public class GetAllPipelinesEndpoint : Endpoint<GetPipelinesRequest, GetPipelinesResponse>
    {
        private readonly PipelineService _pipelineService;

        public GetAllPipelinesEndpoint(PipelineService pipelineService)
        {
            _pipelineService = pipelineService;
        }

        public override void Configure()
        {
            Get("/pipelines");

            Summary(s =>
            {
                s.Summary = "Retrieves all specific pipelines";
                s.Description = "Fetches detailed information about pipelines";

                s.AddGetAllResponses<GetPipelinesResponse>("Pipelines");
            });
        }

        public override async Task<GetPipelinesResponse> ExecuteAsync(GetPipelinesRequest request, CancellationToken ct)
        {
            var dtoPipelines = request.MapToDto();

            var result = _pipelineService.GetAllPipelines(dtoPipelines, ct);

            return await Task.Run(result.MapToResponse);
        }
    }
}
