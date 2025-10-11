// <copyright file="GetCallEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Activity.Mappers.Call;
using BonefireCRM.API.Contrat.Call;
using BonefireCRM.Domain.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BonefireCRM.API.Activity.Endpoints.Call
{
    public class GetCallEndpoint : EndpointWithoutRequest<Results<Ok<GetCallResponse>, NotFound>>
    {
        private readonly ActivityService _activityService;

        public GetCallEndpoint(ActivityService activityService)
        {
            _activityService = activityService;
        }

        public override void Configure()
        {
            Get("/activity/calls/{id:guid}");

            Summary(s =>
            {
                s.Summary = "Retrieves a specific call activity by ID.";
                s.Description = "Fetches detailed information about a call identified by its unique ID.";

                s.Params["id"] = "The unique identifier (GUID) of the call to retrieve.";

                s.Response<Ok<GetCallResponse>>(200, "Call details successfully retrieved.");
                s.Response<NotFound>(404, "The specified call could not be found.");
                s.Response<ProblemDetails>(400, "Invalid request. The provided call ID is not valid.");
                s.Response<UnauthorizedHttpResult>(401, "User is not authorized to access this resource.");
                s.Response<InternalServerError>(500, "An internal server error occurred while retrieving the call details.");
            });
        }

        public override async Task<Results<Ok<GetCallResponse>, NotFound>> ExecuteAsync(CancellationToken ct)
        {
            var id = Route<Guid>("id");

            var result = await _activityService.GetCallAsync(id, ct);

            var response = result.Match<Results<Ok<GetCallResponse>, NotFound>>(
                dtoCall => TypedResults.Ok(dtoCall.MapToResponse()),
                TypedResults.NotFound());

            return response;
        }
    }
}
