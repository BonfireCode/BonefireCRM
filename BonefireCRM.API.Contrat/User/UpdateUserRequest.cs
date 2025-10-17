using System.ComponentModel;

namespace BonefireCRM.API.Contrat.User
{
    public sealed class UpdateUserRequest
    {
        [Description("The updated first name of the user.")]
        public string FirstName { get; set; } = string.Empty;

        [Description("The updated last name of the user.")]
        public string LastName { get; set; } = string.Empty;

        [Description("The updated email address of the user.")]
        public string Email { get; set; } = string.Empty;
    }
}
