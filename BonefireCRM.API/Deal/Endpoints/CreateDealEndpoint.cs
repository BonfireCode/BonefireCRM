// <copyright file="CreateDealEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using System.Security.Claims;
using BonefireCRM.API.Contrat.Deal;
using BonefireCRM.API.Deal.Mappers;
using BonefireCRM.API.Extensions;
using BonefireCRM.Domain.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BonefireCRM.API.Deal.Endpoints
{
    public class CreateDealEndpoint : Endpoint<CreateDealRequest, Results<Created<CreateDealResponse>, InternalServerError>>
    {
        private readonly DealService _dealService;
        private readonly UserService _userService;

        public CreateDealEndpoint(DealService dealService, UserService userService)
        {
            _dealService = dealService;
            _userService = userService;
        }

        public override void Configure()
        {
            Post("/deals");

            Summary(s =>
            {
                s.Summary = "Creates a new deal.";
                s.AddCreateResponses<CreateDealResponse>("Deal");
            });
        }

        public override async Task<Results<Created<CreateDealResponse>, InternalServerError>> ExecuteAsync(CreateDealRequest request, CancellationToken ct)
        {
            var registerId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var userId = await _userService.GetUserIdAsync(registerId, ct);

            var dtoDeal = request.MapToDto(userId);

            var result = await _dealService.CreateDealAsync(dtoDeal, ct);

            var response = result.Match<Results<Created<CreateDealResponse>, InternalServerError>>(
                createdDeal => TypedResults.Created($"/deals/{createdDeal.Id}", createdDeal.MapToResponse()),
                _ => TypedResults.InternalServerError());

            return response;
        }
    }
}
