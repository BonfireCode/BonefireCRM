// <copyright file="GetAllLifecycleStagesEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contrat.LifeCycleStage;
using BonefireCRM.API.Extensions;
using BonefireCRM.API.LifecycleStage.Mappers;
using BonefireCRM.Domain.Services;
using FastEndpoints;

namespace BonefireCRM.API.LifecycleStage.Endpoints
{
    public class GetAllLifecycleStagesEndpoint : Endpoint<GetLifecycleStagesRequest, IEnumerable<GetLifecycleStageResponse>>
    {
        private readonly LifecycleStageService _lifecycleStageService;

        public GetAllLifecycleStagesEndpoint(LifecycleStageService lifecycleStageService)
        {
            _lifecycleStageService = lifecycleStageService;
        }

        public override void Configure()
        {
            Get("/lifecyclestages");
            Summary(s =>
            {
                s.Summary = "Retrieves all specific lifecycle stages";
                s.Description = "Fetches detailed information about lifecycle stages";

                s.AddGetAllResponses<IEnumerable<GetLifecycleStageResponse>>("Lifecycle Stages");
            });
        }

        public override async Task<IEnumerable<GetLifecycleStageResponse>> ExecuteAsync(GetLifecycleStagesRequest request, CancellationToken ct)
        {
            var dtoLifecycleStages = request.MapToDto();

            var result = _lifecycleStageService.GetAllLifecycleStages(dtoLifecycleStages, ct);

            var response = result.Select(c => c.MapToResponse());

            return await Task.Run(() => response);
        }
    }
}
