// <copyright file="RequestToDtoMapper.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contrat.Contact;
using BonefireCRM.Domain.DTOs.Contact;

namespace BonefireCRM.API.Contact.Mappers
{
    internal static class RequestToDtoMapper
    {
        internal static CreateContactDTO MapToDto(CreateContactRequest request)
        {
            return new()
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                JobRole = request.JobRole,
                PhoneNumber = request.PhoneNumber,
            };
        }

        internal static UpdateContactDTO MapToDto(UpdateContactRequest request, Guid id)
        {
            return new()
            {
                Id = id,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                JobRole = request.JobRole,
                PhoneNumber = request.PhoneNumber,
            };
        }
    }
}
