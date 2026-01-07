// <copyright file="UpdateContactValidator.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contrat.Contact;
using FastEndpoints;
using FluentValidation;

namespace BonefireCRM.API.Contact.Validators
{
    public class UpdateContactValidator : Validator<UpdateContactRequest>
    {
        public UpdateContactValidator()
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
