using System.ComponentModel;

namespace BonefireCRM.API.Contrat.Security
{
    public class ResetPasswordRequest
    {
        [Description("The email address of the user requesting a password reset. This should match the email used in the ForgotPasswordRequest.")]
        public required string Email { get; init; }

        [Description("The code sent to the user's email to reset the password. To get this code, first make a '/forgotPassword' request.")]
        public required string ResetCode { get; init; }

        [Description("The new password the user associated with the given email should use to log in. This will replace the previous password.")]
        public required string NewPassword { get; init; }
    }
}
