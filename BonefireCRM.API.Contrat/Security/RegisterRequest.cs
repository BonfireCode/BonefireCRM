using System.ComponentModel;

namespace BonefireCRM.API.Contrat.Security
{
    public sealed class RegisterRequest
    {
        [Description("The username of the user.")]
        public required string UserName { get; set; }

        [Description("The email address of the user.")]
        public required string Email { get; init; }

        [Description("The password of the user.")]
        public required string Password { get; init; }
    }
}
