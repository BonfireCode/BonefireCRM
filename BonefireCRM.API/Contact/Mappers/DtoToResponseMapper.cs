// <copyright file="DtoToResponseMapper.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contrat.Contact;
using BonefireCRM.Domain.DTOs.Contact;

namespace BonefireCRM.API.Contact.Mappers
{
    internal static class DtoToResponseMapper
    {
        internal static GetContactResponse MapToResponse(this GetContactDTO dto)
        {
            return new()
            {
                Id = dto.Id,
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                JobRole = dto.JobRole,
                PhoneNumber = dto.PhoneNumber,
                LifecycleStageId = dto.LifecycleStageId,
                CompanyId = dto.CompanyId,
                UserId = dto.UserId,
            };
        }

        internal static CreateContactResponse MapToResponse(this CreatedContactDTO dto)
        {
            return new()
            {
                Id = dto.Id,
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                JobRole = dto.JobRole,
                PhoneNumber = dto.PhoneNumber,
                CompanyId = dto.CompanyId,
                LifecycleStageId = dto.LifecycleStageId,
                UserId = dto.UserId,
            };
        }

        internal static UpdateContactResponse MapToResponse(this UpdatedContactDTO dto)
        {
            return new()
            {
                Id = dto.Id,
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                JobRole = dto.JobRole,
                PhoneNumber = dto.PhoneNumber,
                LifecycleStageId = dto.LifecycleStageId,
                CompanyId = dto.CompanyId,
                UserId = dto.UserId,
            };
        }
    }
}
