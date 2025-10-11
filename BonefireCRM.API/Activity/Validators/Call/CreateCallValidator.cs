// <copyright file="CreateCallValidator.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contrat.Call;
using FastEndpoints;
using FluentValidation;

namespace BonefireCRM.API.Activity.Validators.Call
{
    public class CreateCallValidator : Validator<CreateCallRequest>
    {
        public CreateCallValidator()
        {
            RuleFor(x => x.ContactId)
                .NotEmpty()
                .WithMessage("this field is required!");

            RuleFor(x => x.CallTime)
                .NotEmpty()
                .WithMessage("this field is required!");

            RuleFor(x => x.Duration)
            .GreaterThan(TimeSpan.Zero)
            .WithMessage("this field must be greater than zero.");

            RuleFor(x => x.Notes)
            .MaximumLength(500)
            .WithMessage("this field cannot exceed 500 characters.");
        }
    }
}
