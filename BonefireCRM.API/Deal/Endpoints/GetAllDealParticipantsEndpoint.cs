// <copyright file="GetAllDealParticipantsEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contrat.Deal;
using BonefireCRM.API.Deal.Mappers;
using BonefireCRM.API.Extensions;
using BonefireCRM.Domain.Services;
using FastEndpoints;

namespace BonefireCRM.API.Deal.Endpoints
{
    public class GetAllDealParticipantsEndpoint : Endpoint<GetDealParticipantsRequest, IEnumerable<GetDealParticipantResponse>>
    {
        private readonly DealService _dealParticipantService;

        public GetAllDealParticipantsEndpoint(DealService dealParticipantService)
        {
            _dealParticipantService = dealParticipantService;
        }

        public override void Configure()
        {
            Get("/deals/{id:guid}/participants");

            Summary(s =>
            {
                s.Summary = "Retrieves all specific dealParticipants";
                s.Description = "Fetches detailed information about dealParticipants";

                s.AddGetAllResponses<IEnumerable<GetDealParticipantResponse>>("Deal participants");
            });
        }

        public override async Task<IEnumerable<GetDealParticipantResponse>> ExecuteAsync(GetDealParticipantsRequest request, CancellationToken ct)
        {
            var id = Route<Guid>("id");
            var dtoDealParticipants = request.MapToDto(id);

            var result = _dealParticipantService.GetAllDealParticipants(dtoDealParticipants, ct);

            var response = result.Select(c => c.MapToResponse());

            return await Task.Run(() => response);
        }
    }
}
