// <copyright file="UpdateDealParticipantRoleValidator.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contrat.DealParticipantRole;
using FastEndpoints;
using FluentValidation;

namespace BonefireCRM.API.DealParticipantRole.Validators
{
    public class UpdateDealParticipantRoleValidator : Validator<UpdateDealParticipantRoleRequest>
    {
        public UpdateDealParticipantRoleValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("this field is required!")
                .MinimumLength(5)
                .WithMessage("this field is too short!");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("this field is required!")
                .MinimumLength(5)
                .WithMessage("this field is too short!");
        }
    }
}
