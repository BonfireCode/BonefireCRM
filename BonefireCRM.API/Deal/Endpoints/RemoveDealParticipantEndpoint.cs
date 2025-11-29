// <copyright file="RemoveDealParticipantEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Extensions;
using BonefireCRM.Domain.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BonefireCRM.API.Deal.Endpoints
{
    public class RemoveDealParticipantEndpoint : EndpointWithoutRequest<Results<NoContent, NotFound>>
    {
        private readonly DealService _dealService;

        public RemoveDealParticipantEndpoint(DealService dealService)
        {
            _dealService = dealService;
        }

        public override void Configure()
        {
            Delete("/deals/{id:guid}/participants/{dealParticipant:guid}");

            Summary(s =>
            {
                s.Summary = "Removes a deal participant from a deal.";
                s.Description = "Removes a deal participant record identified by its unique ID. Returns 204 if deleted successfully, or 404 if the call was not found.";

                s.Params["id"] = "The unique identifier (GUID) of the deal.";
                s.Params["dealParticipant"] = "The unique identifier (GUID) of the deal participant to remove.";

                s.AddDeleteResponses("Deal participant");
            });
        }

        public override async Task<Results<NoContent, NotFound>> ExecuteAsync(CancellationToken ct)
        {
            var id = Route<Guid>("id");
            var dealParticipant = Route<Guid>("dealParticipant");

            var isDeleted = await _dealService.RemoveDealParticipantAsync(id, dealParticipant, ct);
            if (isDeleted)
            {
                return TypedResults.NoContent();
            }

            return TypedResults.NotFound();
        }
    }
}
