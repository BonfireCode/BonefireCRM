// <copyright file="AssignDealParticipantEndpoint.cs" company="Bonefire">
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
    public class AssignDealParticipantEndpoint : Endpoint<AssignDealParticipantRequest, Results<Created<AssignDealParticipantResponse>, InternalServerError>>
    {
        private readonly DealService _dealService;

        public AssignDealParticipantEndpoint(DealService dealService)
        {
            _dealService = dealService;
        }

        public override void Configure()
        {
            Post("/deals/{id:guid}/participants");

            Summary(s =>
            {
                s.Summary = "Assign a participant to a deal.";
                s.Params["id"] = "The unique identifier (GUID) of the deal.";
                s.AddCreateResponses<AssignDealParticipantResponse>("Deal participant");
            });
        }

        public override async Task<Results<Created<AssignDealParticipantResponse>, InternalServerError>> ExecuteAsync(AssignDealParticipantRequest request, CancellationToken ct)
        {
            var id = Route<Guid>("id");
            var dtoDealParticipant = request.MapToDto(id);

            var result = await _dealService.AssignDealParticipantAsync(dtoDealParticipant, ct);

            var response = result.Match<Results<Created<AssignDealParticipantResponse>, InternalServerError>>(
                assignedDealParticipant => TypedResults.Created(string.Empty, assignedDealParticipant.MapToResponse()),
                _ => TypedResults.InternalServerError());

            return response;
        }
    }
}
