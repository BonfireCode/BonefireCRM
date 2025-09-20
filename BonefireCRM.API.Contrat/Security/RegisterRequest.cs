namespace BonefireCRM.API.Contrat.Security
{
    public sealed class RegisterRequest
    {
        /// <summary>
        /// The user's user name.
        /// </summary>
        public required string UserName { get; set; }

        /// <summary>
        /// The user's email address.
        /// </summary>
        public required string Email { get; init; }

        /// <summary>
        /// The user's password.
        /// </summary>
        public required string Password { get; init; }
    }
}
