using BonefireCRM.Domain.DTOs.Email;
using BonefireCRM.Domain.Infrastructure.Email;
using BonefireCRM.Domain.Infrastructure.Security;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.WebUtilities;
using MimeKit;
using MimeKit.Text;
using System.Text;
using System.Text.Encodings.Web;

namespace BonefireCRM.Infrastructure.Email
{
    internal class EmailSender : IEmailSender
    {
        private const string CONFIRM_EMAIL_ENDPOINT_NAME = "/confirmEmail";
        private const string RESET_PASSWORD_ENDPOINT_NAME = "/resetpassword";

        private readonly IAppHttpContextAccessor _appHttpContextAccessor;

        public EmailSender(IAppHttpContextAccessor appHttpContextAccessor)
        {
            _appHttpContextAccessor = appHttpContextAccessor;
        }

        public async Task SendConfirmationEmailAsync(Guid userId, string userEmail, string confirmationToken)
        {
            confirmationToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(confirmationToken));

            var subject = "Email confirmation";
            var actionUrl = $"{_appHttpContextAccessor.GetBaseUrl()}{CONFIRM_EMAIL_ENDPOINT_NAME}?userId={userId}&token={confirmationToken}";

            var email = new EmailDTO
            {
                From = "BonefireCRM",
                To = [userEmail],
                Subject = subject,
                Body = $"""
                Click here to confirm your email: <a href="{HtmlEncoder.Default.Encode(actionUrl)}">HERE</a>
                """,
            };

            await SendEmailAsync(email);
        }

        public async Task SendPasswordResetEmailAsync(string resetToken, string userEmail)
        {
            resetToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(resetToken));

            var actionUrl = $"{_appHttpContextAccessor.GetBaseUrl()}{RESET_PASSWORD_ENDPOINT_NAME}?token={resetToken}";

            var email = new EmailDTO
            {
                From = "BonefireCRM",
                To = [userEmail],
                Subject = "Password reset",
                Body = $"""
                Click here to reset your password: <a href="{HtmlEncoder.Default.Encode(actionUrl)}">HERE</a>
                """,
            };

            await SendEmailAsync(email);
        }

        public async Task SendChangeEmailAsync(Guid userId, string userEmail, string changeToken)
        {
            changeToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(changeToken));

            var subject = "Email change";
            var actionUrl = $"{_appHttpContextAccessor.GetBaseUrl()}{CONFIRM_EMAIL_ENDPOINT_NAME}?userId={userId}&token={changeToken}&changedEmail={userEmail}";

            var email = new EmailDTO
            {
                From = "BonefireCRM",
                FromName = "BonefireCRM",
                To = [userEmail],
                Subject = subject,
                Body = $"""
                Click here to change your email: <a href="{HtmlEncoder.Default.Encode(actionUrl)}">HERE</a>
                """,
            };

            await SendEmailAsync(email);
        }

        private async Task SendEmailAsync(EmailDTO emailDTO)
        {
            var message = new MimeMessage();

            var from = new MailboxAddress(emailDTO.FromName, emailDTO.From);
            message.From.Add(from);

            var to = emailDTO.To.Select(x => new MailboxAddress("USERS", x)) ;
            message.To.AddRange(to);

            message.Subject = emailDTO.Subject;

            message.Body = new TextPart(TextFormat.Html) { Text = emailDTO.Body };

            using (var smtpClient = new SmtpClient())
            {
                //THIS CONFIG IS ONLY FOR TESTING PURPOSES WITH MAILPIT
                await smtpClient.ConnectAsync("localhost", 1025);
                await smtpClient.SendAsync(message);
                await smtpClient.DisconnectAsync(true);
            }
        }
    }
}
