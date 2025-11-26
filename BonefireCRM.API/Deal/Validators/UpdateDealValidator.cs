// <copyright file="UpdateDealValidator.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contrat.Deal;
using FastEndpoints;
using FluentValidation;

namespace BonefireCRM.API.Deal.Validators
{
    public class UpdateDealValidator : Validator<UpdateDealRequest>
    {
        public UpdateDealValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("this field is required!")
                .MinimumLength(5)
                .WithMessage("this field is too short!");

            RuleFor(x => x)
                .Must(x =>
                {
                    bool hasCompany = x.CompanyId.HasValue;
                    bool hasPrimary = x.PrimaryContactId.HasValue;

                    // Operator (^) : returns true if only one of the operands is true
                    return hasCompany ^ hasPrimary;
                })
                .WithMessage("Either company or primary contact must be provided, but not both.");
        }
    }
}
