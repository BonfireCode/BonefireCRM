using BonefireCRM.API.Contrat;
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
