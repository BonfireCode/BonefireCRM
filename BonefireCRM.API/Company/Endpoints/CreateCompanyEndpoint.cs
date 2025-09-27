// <copyright file="CreateCompanyEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Company.Mappers;
using BonefireCRM.API.Contrat.Company;
using BonefireCRM.Domain.DTOs.Company;
using BonefireCRM.Domain.Services;
using FastEndpoints;
using LanguageExt.Common;
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
        }

        public override async Task<Results<Created<CreateCompanyResponse>, InternalServerError>> ExecuteAsync(CreateCompanyRequest request, CancellationToken ct)
        {
            var dtoCompany = RequestToDtoMapper.MapToDto(request);

            var result = await _companyService.CreateCompanyAsync(dtoCompany, ct);

            var response = result.Match<Results<Created<CreateCompanyResponse>, InternalServerError>>(
                createdCompany => TypedResults.Created($"/Companys/{createdCompany.Id}", createdCompany.MapToResponse()),
                _ => TypedResults.InternalServerError());

            return response;
        }
    }
}
