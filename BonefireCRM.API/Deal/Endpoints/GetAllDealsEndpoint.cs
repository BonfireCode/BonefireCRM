// <copyright file="GetAllDealsEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using System.Security.Claims;
using BonefireCRM.API.Contrat.Deal;
using BonefireCRM.API.Deal.Mappers;
using BonefireCRM.API.Extensions;
using BonefireCRM.Domain.Services;
using FastEndpoints;

namespace BonefireCRM.API.Deal.Endpoints
{
    public class GetAllDealsEndpoint : Endpoint<GetDealsRequest, IEnumerable<GetDealResponse>>
    {
        private readonly DealService _dealService;
        private readonly UserService _userService;

        public GetAllDealsEndpoint(DealService dealService, UserService userService)
        {
            _dealService = dealService;
            _userService = userService;
        }

        public override void Configure()
        {
            Get("/deals");

            Summary(s =>
            {
                s.Summary = "Retrieves all specific deals";
                s.Description = "Fetches detailed information about deals";

                s.AddGetAllResponses<IEnumerable<GetDealResponse>>("Deals");
            });
        }

        public override async Task<IEnumerable<GetDealResponse>> ExecuteAsync(GetDealsRequest request, CancellationToken ct)
        {
            var registerId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var userId = await _userService.GetUserIdAsync(registerId, ct);

            var dtoDeals = request.MapToDto(userId);

            var result = _dealService.GetAllDeals(dtoDeals, ct);

            var response = result.Select(c => c.MapToResponse());

            return await Task.Run(() => response);
        }
    }
}
