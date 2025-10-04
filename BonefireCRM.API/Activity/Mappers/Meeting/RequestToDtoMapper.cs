// <copyright file="RequestToDtoMapper.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contrat.Meeting;
using BonefireCRM.Domain.DTOs.Activity.Meeting;

namespace BonefireCRM.API.Company.Mappers.Meeting
{
    internal static class RequestToDtoMapper
    {
        internal static CreateMeetingDTO MapToDto(CreateMeetingRequest request)
        {
            return new()
            {
            };
        }

        internal static UpdateMeetingDTO MapToDto(UpdateMeetingRequest request, Guid id)
        {
            return new()
            {
            };
        }
    }
}
