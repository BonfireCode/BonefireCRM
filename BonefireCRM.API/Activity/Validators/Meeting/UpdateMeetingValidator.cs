// <copyright file="UpdateMeetingValidator.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contrat.Meeting;
using FastEndpoints;
using FluentValidation;

namespace BonefireCRM.API.Activity.Validators.Meeting
{
    public class UpdateMeetingValidator : Validator<UpdateMeetingRequest>
    {
        public UpdateMeetingValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("this field is required!");

            RuleFor(x => x.ContactId)
                .NotEmpty()
                .WithMessage("this field is required!");

            RuleFor(x => x.Subject)
                .NotEmpty()
                .WithMessage("this field is required!")
                .MaximumLength(200)
                .WithMessage("this field cannot exceed 200 characters.");

            RuleFor(x => x.StartTime)
                .NotEmpty()
                .WithMessage("this field is required!");

            RuleFor(x => x.EndTime)
                .NotEmpty()
                .WithMessage("this field is required!");

            RuleFor(x => x.Notes)
            .MaximumLength(2000)
            .WithMessage("this field cannot exceed 2000 characters.");
        }
    }
}
