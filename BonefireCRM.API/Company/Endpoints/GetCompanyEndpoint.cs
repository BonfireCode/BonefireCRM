// <copyright file="GetCompanyEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Company.Mappers;
using BonefireCRM.API.Contrat.Call;
using BonefireCRM.API.Contrat.Company;
using BonefireCRM.API.Extensions;
using BonefireCRM.Domain.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BonefireCRM.API.Company.Endpoints
{
    public class GetCompanyEndpoint : EndpointWithoutRequest<Results<Ok<GetCompanyResponse>, NotFound>>
    {
        private readonly CompanyService _companyService;

        public GetCompanyEndpoint(CompanyService companyService)
        {
            _companyService = companyService;
        }

        public override void Configure()
        {
            Get("/companies/{id:guid}");

            Summary(s =>
            {
                s.Summary = "Retrieves a specific company by ID.";
                s.Description = "Fetches detailed information about a company identified by its unique ID.";

                s.Params["id"] = "The unique identifier (GUID) of the company to retrieve.";

                s.AddGetResponses<GetCompanyResponse>("Company");
            });
        }

        public override async Task<Results<Ok<GetCompanyResponse>, NotFound>> ExecuteAsync(CancellationToken ct)
        {
            var id = Route<Guid>("id");
            var result = await _companyService.GetCompanyAsync(id, ct);

            var response = result.Match<Results<Ok<GetCompanyResponse>, NotFound>>(
                dtoCompany => TypedResults.Ok(dtoCompany.MapToResponse()),
                TypedResults.NotFound());

            return response;
        }
    }
}
