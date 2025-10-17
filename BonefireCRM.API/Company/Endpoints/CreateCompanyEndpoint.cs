// <copyright file="CreateCompanyEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Company.Mappers;
using BonefireCRM.API.Contrat.Company;
using BonefireCRM.API.Extensions;
using BonefireCRM.Domain.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BonefireCRM.API.Company.Endpoints
{
    public class CreateCompanyEndpoint : Endpoint<CreateCompanyRequest, Results<Created<CreateCompanyResponse>, InternalServerError>>
    {
        private readonly CompanyService _companyService;

        public CreateCompanyEndpoint(CompanyService companyService)
        {
            _companyService = companyService;
        }

        public override void Configure()
        {
            Post("/companies");

            Summary(s =>
            {
                s.Summary = "Creates a new call company.";
                s.AddCreateResponses<CreateCompanyResponse>("Company");
            });
        }

        public override async Task<Results<Created<CreateCompanyResponse>, InternalServerError>> ExecuteAsync(CreateCompanyRequest request, CancellationToken ct)
        {
            var dtoCompany = request.MapToDto();

            var result = await _companyService.CreateCompanyAsync(dtoCompany, ct);

            var response = result.Match<Results<Created<CreateCompanyResponse>, InternalServerError>>(
                createdCompany => TypedResults.Created($"/Companys/{createdCompany.Id}", createdCompany.MapToResponse()),
                _ => TypedResults.InternalServerError());

            return response;
        }
    }
}
