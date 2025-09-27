// <copyright file="UpdateCompanyEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Company.Mappers;
using BonefireCRM.API.Contrat.Company;
using BonefireCRM.Domain.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BonefireCRM.API.Company.Endpoints
{
    public class UpdateCompanyEndpoint : Endpoint<UpdateCompanyRequest, Results<Created<UpdateCompanyResponse>, InternalServerError>>
    {
        private readonly CompanyService _companyService;

        public UpdateCompanyEndpoint(CompanyService companyService)
        {
            _companyService = companyService;
        }

        public override void Configure()
        {
            Put("/companies/{id:guid}");
        }

        public override async Task<Results<Created<UpdateCompanyResponse>, InternalServerError>> ExecuteAsync(UpdateCompanyRequest request, CancellationToken ct)
        {
            var id = Route<Guid>("id");
            var dtoCompany = RequestToDtoMapper.MapToDto(request, id);
            var result = await _companyService.UpdateCompanyAsync(dtoCompany, ct);

            var response = result.Match<Results<Created<UpdateCompanyResponse>, InternalServerError>>(
                updatedCompany => TypedResults.Created($"/companies/{updatedCompany.Id}", updatedCompany.MapToResponse()),
                _ => TypedResults.InternalServerError());

            return response;
        }
    }
}
