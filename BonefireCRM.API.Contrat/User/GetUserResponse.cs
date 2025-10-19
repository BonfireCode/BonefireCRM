using BonefireCRM.API.Contrat.Contact;
using System.ComponentModel;

namespace BonefireCRM.API.Contrat.User
{
    public sealed class GetUserResponse
    {
        [Description("The unique identifier of the user.")]
        public Guid Id { get; set; }

        [Description("The first name of the user.")]
        public string FirstName { get; set; } = string.Empty;

        [Description("The last name of the user.")]
        public string LastName { get; set; } = string.Empty;

        [Description("The email address of the user.")]
        public string Email { get; set; } = string.Empty;

        [Description("A collection of contacts associated with the user.")]
        public IEnumerable<GetContactResponse> Contacts { get; set; } = [];
    }
}
