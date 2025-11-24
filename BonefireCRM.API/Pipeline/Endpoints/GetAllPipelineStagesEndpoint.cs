// <copyright file="GetAllPipelineStagesEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contrat.PipelineStage;
using BonefireCRM.API.Extensions;
using BonefireCRM.API.PipelineStage.Mappers;
using BonefireCRM.Domain.Services;
using FastEndpoints;

namespace BonefireCRM.API.Pipeline.Endpoints
{
    public class GetAllPipelineStagesEndpoint : Endpoint<GetPipelineStagesRequest, IEnumerable<GetPipelineStageResponse>>
    {
        private readonly PipelineStageService _pipelineStageService;

        public GetAllPipelineStagesEndpoint(PipelineStageService pipelineStageService)
        {
            _pipelineStageService = pipelineStageService;
        }

        public override void Configure()
        {
            Get("/pipelines/{id:guid}/pipelinestages");
            Summary(s =>
            {
                s.Summary = "Retrieves all specific pipeline stages";
                s.Description = "Fetches detailed information about pipeline stages";
                s.Params["id"] = "The unique identifier (GUID) of the pipeline whose stages are being retrieved.";

                s.AddGetAllResponses<IEnumerable<GetPipelineStageResponse>>("Pipeline Stages");
            });
        }

        public override async Task<IEnumerable<GetPipelineStageResponse>> ExecuteAsync(GetPipelineStagesRequest request, CancellationToken ct)
        {
            var id = Route<Guid>("id");
            var dtoPipelineStages = request.MapToDto(id);

            var result = _pipelineStageService.GetAllPipelineStages(dtoPipelineStages, ct);

            var response = result.Select(c => c.MapToResponse());

            return await Task.Run(() => response);
        }
    }
}
