using BonefireCRM.Domain.DTOs.Email;

namespace BonefireCRM.Domain.Infrastructure.Email
{
    public interface IEmailSender
    {
        Task SendConfirmationEmailAsync(string userId, string userEmail, string confirmationToken);

        Task SendPasswordResetEmailAsync(string resetToken, string userEmail);

        Task SendChangeEmailAsync(string userId, string userEmail, string changeToken);
    }
}
