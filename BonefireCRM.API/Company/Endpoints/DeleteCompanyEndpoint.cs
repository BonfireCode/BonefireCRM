// <copyright file="DeleteCompanyEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Extensions;
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
            Delete("/companies/{id:guid}");

            Summary(s =>
            {
                s.Summary = "Deletes an existing company.";
                s.Description = "Removes a company record identified by its unique ID. Returns 204 if deleted successfully, or 404 if the call was not found.";

                s.Params["id"] = "The unique identifier (GUID) of the call to delete.";

                s.AddDeleteResponses("Company");
            });
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
