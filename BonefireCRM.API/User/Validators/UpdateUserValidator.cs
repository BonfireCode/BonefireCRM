// <copyright file="UpdateUserValidator.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contrat.User;
using FastEndpoints;
using FluentValidation;

namespace BonefireCRM.API.User.Validators
{
    public class UpdateUserValidator : Validator<UpdateUserRequest>
    {
        public UpdateUserValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("this field is required!")
                .MinimumLength(3)
                .WithMessage("this field is too short!");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("this field is required!")
                .MinimumLength(3)
                .WithMessage("this field is too short!");
        }
    }
}
