using BonefireCRM.API.Contrat.Contact;
using System.ComponentModel;

namespace BonefireCRM.API.Contrat.User
{
    public sealed class UpdateUserResponse
    {
        [Description("The unique identifier of the updated user.")]
        public Guid Id { get; set; }

        [Description("The updated first name of the user.")]
        public string FirstName { get; set; } = string.Empty;

        [Description("The updated last name of the user.")]
        public string LastName { get; set; } = string.Empty;

        [Description("The updated email address of the user.")]
        public string Email { get; set; } = string.Empty;

        [Description("A collection of the user's updated contacts.")]
        public IEnumerable<UpdateContactResponse> Contacts { get; set; } = [];
    }
}
