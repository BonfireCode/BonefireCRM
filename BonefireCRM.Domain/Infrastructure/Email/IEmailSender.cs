using BonefireCRM.Domain.DTOs.Email;

namespace BonefireCRM.Domain.Infrastructure.Email
{
    public interface IEmailSender
    {
        Task SendConfirmationEmailAsync(Guid userId, string userEmail, string confirmationToken);

        Task SendPasswordResetEmailAsync(string resetToken, string userEmail);

        Task SendChangeEmailAsync(Guid userId, string userEmail, string changeToken);
    }
}
