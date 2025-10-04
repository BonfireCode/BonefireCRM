// <copyright file="DtoToResponseMapper.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>
using BonefireCRM.API.Contrat.Meeting;
using BonefireCRM.Domain.DTOs.Activity.Meeting;

namespace BonefireCRM.API.Company.Mappers.Meeting
{
    internal static class DtoToResponseMapper
    {
        internal static GetMeetingResponse MapToResponse(this GetMeetingDTO dto)
        {
            return new()
            {
            };
        }

        internal static CreateMeetingResponse MapToResponse(this CreatedMeetingDTO dto)
        {
            return new()
            {
            };
        }

        internal static UpdateMeetingResponse MapToResponse(this UpdatedMeetingDTO dto)
        {
            return new()
            {
            };
        }
    }
}
