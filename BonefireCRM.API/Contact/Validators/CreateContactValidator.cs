// <copyright file="CreateContactValidator.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contrat.Contact;
using FastEndpoints;
using FluentValidation;

namespace BonefireCRM.API.Contact.Validators
{
    public class CreateContactValidator : Validator<CreateContactRequest>
    {
        public CreateContactValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("your FirstName is required!")
                .MinimumLength(5)
                .WithMessage("your FirstName is too short!");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("your LastName is required!")
                .MinimumLength(5)
                .WithMessage("your LastName is too short!");
        }
    }
}
