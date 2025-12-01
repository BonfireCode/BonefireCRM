// <copyright file="GetAllCallsEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Activity.Mappers.Call;
using BonefireCRM.API.Contrat.Call;
using BonefireCRM.API.Extensions;
using BonefireCRM.Domain.Services;
using FastEndpoints;

namespace BonefireCRM.API.Activity.Endpoints.Call
{
    public class GetAllCallsEndpoint : Endpoint<GetCallsRequest, IEnumerable<GetCallResponse>>
    {
        private readonly ActivityService _activityService;

        public GetAllCallsEndpoint(ActivityService activityService)
        {
            _activityService = activityService;
        }

        public override void Configure()
        {
            Get("/activity/calls");

            Summary(s =>
            {
                s.Summary = "Retrieves all specific calls";
                s.Description = "Fetches detailed information about calls";

                s.AddGetAllResponses<IEnumerable<GetCallResponse>>("Calls");
            });
        }

        public override async Task<IEnumerable<GetCallResponse>> ExecuteAsync(GetCallsRequest request, CancellationToken ct)
        {
            var dtoCalls = request.MapToDto();

            var result = _activityService.GetAllCalls(dtoCalls, ct);

            var response = result.Select(c => c.MapToResponse());

            return await System.Threading.Tasks.Task.Run(() => response);
        }
    }
}