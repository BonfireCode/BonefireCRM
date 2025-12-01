// <copyright file="GetDealEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contrat.Deal;
using BonefireCRM.API.Deal.Mappers;
using BonefireCRM.API.Extensions;
using BonefireCRM.Domain.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BonefireCRM.API.Deal.Endpoints
{
    public class GetDealEndpoint : EndpointWithoutRequest<Results<Ok<GetDealResponse>, NotFound>>
    {
        private readonly DealService _dealService;

        public GetDealEndpoint(DealService dealService)
        {
            _dealService = dealService;
        }

        public override void Configure()
        {
            Get("/deals/{id:guid}");

            Summary(s =>
            {
                s.Summary = "Retrieves a specific deal by ID.";
                s.Description = "Fetches detailed information about a deal identified by its unique ID.";

                s.Params["id"] = "The unique identifier (GUID) of the deal to retrieve.";

                s.AddGetResponses<GetDealResponse>("Deal");
            });
        }

        public override async Task<Results<Ok<GetDealResponse>, NotFound>> ExecuteAsync(CancellationToken ct)
        {
            var id = Route<Guid>("id");

            var result = await _dealService.GetDealAsync(id, ct);

            var response = result.Match<Results<Ok<GetDealResponse>, NotFound>>(
                dtoDeal => TypedResults.Ok(dtoDeal.MapToResponse()),
                TypedResults.NotFound());

            return response;
        }
    }
}
