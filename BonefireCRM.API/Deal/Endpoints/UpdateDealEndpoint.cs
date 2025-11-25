// <copyright file="UpdateDealEndpoint.cs" company="Bonefire">
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
    public class UpdateDealEndpoint : Endpoint<UpdateDealRequest, Results<Ok<UpdateDealResponse>, NotFound, InternalServerError>>
    {
        private readonly DealService _dealService;
        private readonly UserService _userService;

        public UpdateDealEndpoint(DealService dealService, UserService userService)
        {
            _dealService = dealService;
            _userService = userService;
        }

        public override void Configure()
        {
            Put("/deals/{id:guid}");

            Summary(s =>
            {
                s.Summary = "Updates an existing deal.";
                s.Description = "Updates the details of an existing deal identified by its unique ID.";
                s.Params["id"] = "The unique identifier (GUID) of the deal to update.";
                s.AddUpdateResponses<UpdateDealResponse>("Contat");
            });
        }

        public override async Task<Results<Ok<UpdateDealResponse>, NotFound, InternalServerError>> ExecuteAsync(UpdateDealRequest request, CancellationToken ct)
        {
            var id = Route<Guid>("id");
            var registerId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var userId = await _userService.GetUserIdAsync(registerId, ct);

            var dtoDeal = request.MapToDto(id, userId);

            var result = await _dealService.UpdateDealAsync(dtoDeal, ct);

            var response = result.Match<Results<Ok<UpdateDealResponse>, NotFound, InternalServerError>>(
                updatedDeal => TypedResults.Ok(updatedDeal.MapToResponse()),
                _ => TypedResults.InternalServerError());

            return response;
        }
    }
}
