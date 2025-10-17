// <copyright file="LogoutEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.Domain.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BonefireCRM.API.Security.Endpoints
{
    public class LogoutEndpoint : Endpoint<EmptyRequest, Results<Ok, UnauthorizedHttpResult>>
    {
        private readonly SecurityService _securityService;

        public LogoutEndpoint(SecurityService securityService)
        {
            _securityService = securityService;
        }

        public override void Configure()
        {
            Post("/security/logout");

            Summary(s =>
            {
                s.Summary = "Logs out the currently authenticated user.";
                s.Description = "Ends the authenticated user session by clearing authentication cookies or revoking bearer tokens. " +
                                "If the user is not authenticated, an unauthorized response is returned.";

                s.Response<Ok>(200, "User successfully logged out. Authentication cookies or tokens are invalidated.");
                s.Response<UnauthorizedHttpResult>(401, "User is not authenticated or the session has already expired.");
                s.Response<InternalServerError>(500, "An internal server error occurred while processing the logout request.");
            });
        }

        public override async Task<Results<Ok, UnauthorizedHttpResult>> ExecuteAsync(EmptyRequest request, CancellationToken ct)
        {
            if (User.Identity!.IsAuthenticated)
            {
                await _securityService.LogoutUser(ct);
                return TypedResults.Ok();
            }

            return TypedResults.Unauthorized();
        }
    }
}
