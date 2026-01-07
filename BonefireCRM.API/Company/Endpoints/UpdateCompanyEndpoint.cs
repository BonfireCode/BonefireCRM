// <copyright file="UpdateCompanyEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using System.Security.Claims;
using BonefireCRM.API.Company.Mappers;
using BonefireCRM.API.Contrat.Call;
using BonefireCRM.API.Contrat.Company;
using BonefireCRM.API.Extensions;
using BonefireCRM.Domain.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BonefireCRM.API.Company.Endpoints
{
    public class UpdateCompanyEndpoint : Endpoint<UpdateCompanyRequest, Results<Ok<UpdateCompanyResponse>, NotFound>>
    {
        private readonly CompanyService _companyService;
        private readonly UserService _userService;

        public UpdateCompanyEndpoint(CompanyService companyService, UserService userService)
        {
            _companyService = companyService;
            _userService = userService;
        }

        public override void Configure()
        {
            Put("/companies/{id:guid}");

            Summary(s =>
            {
                s.Summary = "Updates an existing company.";
                s.Description = "Updates the details of an existing company identified by its unique ID.";
                s.Params["id"] = "The unique identifier (GUID) of the company to update.";
                s.AddUpdateResponses<UpdateCompanyResponse>("Company");
            });
        }

        public override async Task<Results<Ok<UpdateCompanyResponse>, NotFound>> ExecuteAsync(UpdateCompanyRequest request, CancellationToken ct)
        {
            var id = Route<Guid>("id");

            var registerId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var userId = await _userService.GetUserIdAsync(registerId, ct);

            var dtoCompany = request.MapToDto(id, userId);

            var result = await _companyService.UpdateCompanyAsync(dtoCompany, ct);

            var response = result.Match<Results<Ok<UpdateCompanyResponse>, NotFound>>(
                updatedCompany => TypedResults.Ok(updatedCompany.MapToResponse()),
                _ => TypedResults.NotFound());

            return response;
        }
    }
}
