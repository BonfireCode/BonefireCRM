// <copyright file="DeleteCompanyEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.Domain.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BonefireCRM.API.Company.Endpoints
{
    public class DeleteCompanyEndpoint : EndpointWithoutRequest<Results<NoContent, NotFound>>
    {
        private readonly CompanyService _companyService;

        public DeleteCompanyEndpoint(CompanyService companyService)
        {
            _companyService = companyService;
        }

        public override void Configure()
        {
            Delete("/companies/{id}");
        }

        public override async Task<Results<NoContent, NotFound>> ExecuteAsync(CancellationToken ct)
        {
            var id = Route<Guid>("id");
            var isDeleted = await _companyService.DeleteCompanyAsync(id, ct);

            if (isDeleted)
            {
                return TypedResults.NoContent();
            }

            return TypedResults.NotFound();
        }
    }
}
