// <copyright file="GetAllUsersEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contrat.User;
using BonefireCRM.API.Extensions;
using BonefireCRM.API.User.Mappers;
using BonefireCRM.Domain.Services;
using FastEndpoints;

namespace BonefireCRM.API.User.Endpoints
{
    public class GetAllUsersEndpoint : Endpoint<GetUsersRequest, IEnumerable<GetUserResponse>>
    {
        private readonly UserService _userService;

        public GetAllUsersEndpoint(UserService userService)
        {
            _userService = userService;
        }

        public override void Configure()
        {
            Get("/users");

            Summary(s =>
            {
                s.Summary = "Retrieves all specific users";
                s.Description = "Fetches detailed information about users";

                s.AddGetAllResponses<IEnumerable<GetUserResponse>>("Users");
            });
        }

        public override async Task<IEnumerable<GetUserResponse>> ExecuteAsync(GetUsersRequest request, CancellationToken ct)
        {
            var dtoUsers = request.MapToDto();

            var result = _userService.GetAllUsers(dtoUsers, ct);

            var response = result.Select(u => u.MapToResponse());

            return await Task.Run(() => response);
        }
    }
}