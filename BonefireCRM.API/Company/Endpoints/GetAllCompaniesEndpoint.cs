// <copyright file="GetAllCompaniesEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Company.Mappers;
using BonefireCRM.API.Contrat.Company;
using BonefireCRM.API.Extensions;
using BonefireCRM.Domain.Services;
using FastEndpoints;

namespace BonefireCRM.API.Company.Endpoints
{
    public class GetAllCompaniesEndpoint : Endpoint<GetCompaniesRequest, IEnumerable<GetCompanyResponse>>
    {
        private readonly CompanyService _companyService;

        public GetAllCompaniesEndpoint(CompanyService companyService)
        {
            _companyService = companyService;
        }

        public override void Configure()
        {
            Get("/companies");

            Summary(s =>
            {
                s.Summary = "Retrieves all specific companies";
                s.Description = "Fetches detailed information about companies";

                s.AddGetAllResponses<IEnumerable<GetCompanyResponse>>("Companies");
            });
        }

        public override async Task<IEnumerable<GetCompanyResponse>> ExecuteAsync(GetCompaniesRequest request, CancellationToken ct)
        {
            var dtoCompanies = request.MapToDto();

            var result = _companyService.GetAllCompanies(dtoCompanies, ct);

            var response = result.Select(c => c.MapToResponse());

            return await Task.Run(() => response);
        }
    }
}