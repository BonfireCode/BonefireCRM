// <copyright file="DeleteDealEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Extensions;
using BonefireCRM.Domain.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BonefireCRM.API.Deal.Endpoints
{
    public class DeleteDealEndpoint : EndpointWithoutRequest<Results<NoContent, NotFound>>
    {
        private readonly DealService _dealService;

        public DeleteDealEndpoint(DealService dealService)
        {
            _dealService = dealService;
        }

        public override void Configure()
        {
            Delete("/deals/{id:guid}");

            Summary(s =>
            {
                s.Summary = "Deletes an existing deal.";
                s.Description = "Removes a deal record identified by its unique ID. Returns 204 if deleted successfully, or 404 if the call was not found.";

                s.Params["id"] = "The unique identifier (GUID) of the deal to delete.";

                s.AddDeleteResponses("Deal");
            });
        }

        public override async Task<Results<NoContent, NotFound>> ExecuteAsync(CancellationToken ct)
        {
            var id = Route<Guid>("id");

            var isDeleted = await _dealService.DeleteDealAsync(id, ct);
            if (isDeleted)
            {
                return TypedResults.NoContent();
            }

            return TypedResults.NotFound();
        }
    }
}
