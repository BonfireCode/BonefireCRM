// <copyright file="GetContactsValidator.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contrat.Contact;
using FastEndpoints;
using FluentValidation;

namespace BonefireCRM.API.Contact.Validators
{
    public class GetContactsValidator : Validator<GetContactsRequest>
    {
        public GetContactsValidator()
        {
            RuleFor(x => x.PageSize)
            .Must(size => new[] { 30, 50, 100 }.Contains(size))
            .WithMessage("Page size must be one of the following values: 30, 50, or 100.");

            RuleFor(x => x.PageNumber)
                .GreaterThan(0)
                .WithMessage("Page number must be greater than zero.");

            RuleFor(x => x.SortBy)
            .Must(sortBy =>
            {
                if (string.IsNullOrWhiteSpace(sortBy))
                {
                    return true;
                }

                var allowed = new[]
                {
                    nameof(GetContactsRequest.Id),
                    nameof(GetContactsRequest.FirstName),
                    nameof(GetContactsRequest.LastName),
                };

                return allowed.Contains(sortBy, StringComparer.OrdinalIgnoreCase);
            })
            .WithMessage("SortBy must be one of the following: Id, FirstName, or LastName.");

            RuleFor(x => x.SortDirection)
                .Must(d => string.Equals(d, "asc", StringComparison.OrdinalIgnoreCase)
                        || string.Equals(d, "desc", StringComparison.OrdinalIgnoreCase))
                .WithMessage("Sort direction must be either 'asc' or 'desc'.");

            RuleFor(x => x.FirstName)
                .MaximumLength(100)
                .WithMessage("First name cannot exceed 100 characters.");

            RuleFor(x => x.LastName)
                .MaximumLength(100)
                .WithMessage("Last name cannot exceed 100 characters.");

            RuleFor(x => x.Id)
                .Must(id => id == null || id != Guid.Empty)
                .WithMessage("If provided, Id must be a valid GUID.");

        }
    }
}
