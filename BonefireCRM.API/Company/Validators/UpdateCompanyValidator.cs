// <copyright file="UpdateCompanyValidator.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contrat.Company;
using FastEndpoints;
using FluentValidation;

namespace BonefireCRM.API.Company.Validators
{
    public class UpdateCompanyValidator : Validator<UpdateCompanyRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateCompanyValidator"/> class.
        /// </summary>
        public UpdateCompanyValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("this field is required!")
                .MinimumLength(5)
                .WithMessage("this field is too short!")
                .MaximumLength(200)
                .WithMessage("this field is too long!");

            RuleFor(x => x.Industry)
                .MaximumLength(100)
                .WithMessage("this field is too long!");

            RuleFor(x => x.Address)
                .MaximumLength(300)
                .WithMessage("this field is too long!");

            RuleFor(x => x.PhoneNumber)
                .MaximumLength(50)
                .WithMessage("this field is too long!");
        }
    }
}
