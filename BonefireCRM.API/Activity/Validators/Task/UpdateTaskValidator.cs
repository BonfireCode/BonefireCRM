// <copyright file="UpdateTaskValidator.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contrat.Activity.Assignment;
using FastEndpoints;
using FluentValidation;

namespace BonefireCRM.API.Activity.Validators.Task
{
    public class UpdateTaskValidator : Validator<UpdateAssignmentRequest>
    {
        public UpdateTaskValidator()
        {
            RuleFor(x => x.ContactId)
                .NotEmpty()
                .WithMessage("this field is required!");

            RuleFor(x => x.Subject)
                .NotEmpty()
                .WithMessage("this field is required!")
                .MaximumLength(200)
                .WithMessage("this field cannot exceed 200 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(2000)
                .WithMessage("this field cannot exceed 2000 characters.");

            RuleFor(x => x.DueDate)
                .GreaterThanOrEqualTo(_ => DateTime.UtcNow)
                .WithMessage("this field cannot be in the past (UTC).");
        }
    }
}
