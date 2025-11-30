// <copyright file="RequestToDtoMapper.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contrat.Activity.Assignment;
using BonefireCRM.Domain.Constants;
using BonefireCRM.Domain.DTOs.Activity.Assignment;

namespace BonefireCRM.API.Activity.Mappers.Assignment
{
    internal static class RequestToDtoMapper
    {
        internal static GetAllAssignmentsDTO MapToDto(this GetAssignmentsRequest request)
        {
            return new()
            {
                Id = request.Id,
                Subject = request.Subject,
                Description = request.Description,
                DueDate = request.DueDate,
                IsCompleted = request.IsCompleted,
                UserId = request.UserId,
                ContactId = request.ContactId,
                CompanyId = request.CompanyId,
                DealId = request.DealId,
                SortBy = request.SortBy ?? nameof(request.Id),
                SortDirection = request.SortDirection ?? DefaultValues.SORTDIRECTION,
                PageNumber = request.PageNumber ?? DefaultValues.PAGENUMBER,
                PageSize = request.PageSize ?? DefaultValues.PAGESIZE,
            };
        }

        internal static CreateAssignmentDTO MapToDto(this CreateAssignmentRequest request, Guid userId)
        {
            return new()
            {
                CompanyId = request.CompanyId,
                ContactId = request.ContactId,
                DealId = request.DealId,
                Subject = request.Subject,
                Description = request.Description,
                DueDate = request.DueDate,
                IsCompleted = request.IsCompleted,
                UserId = userId,
            };
        }

        internal static UpdateAssignmentDTO MapToDto(this UpdateAssignmentRequest request, Guid id, Guid userId)
        {
            return new()
            {
                Id = id,
                CompanyId = request.CompanyId,
                ContactId = request.ContactId,
                DealId = request.DealId,
                Subject = request.Subject,
                Description = request.Description,
                DueDate = request.DueDate,
                IsCompleted = request.IsCompleted,
                UserId = userId,
            };
        }
    }
}
