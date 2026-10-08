using System.ComponentModel;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace NewsSummarizer.Infrastructure.Smtp
{
    internal class SmtpHandler(IConfiguration configuration) : ISmtpHandler
    {
        private readonly IConfiguration _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        
        private const string TARGET_EMAIL = "INSERT EMAIL HERE";
        
        [Description("Sends an email with the specified content.")]
        public async Task SendEmail( [Description("The content of the email to send.")] string content)
        {
            var _smtpUser = _configuration["Smtp:User"];
            var _smtpPassword = _configuration["Smtp:Password"];
          
            var message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse(_smtpUser));
            message.To.Add(MailboxAddress.Parse(TARGET_EMAIL));
            message.Subject = $"News Digest: Daily Update {DateTime.Now:yyyy-MM-dd}";
            message.Body = new TextPart("html") { Text = content };

            using var client = new SmtpClient();
            await client.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(_smtpUser, _smtpPassword);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}
