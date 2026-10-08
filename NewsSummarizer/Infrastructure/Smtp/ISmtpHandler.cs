using System.ComponentModel;

namespace NewsSummarizer.Infrastructure.Smtp
{
    internal interface ISmtpHandler
    {
        [Description("Sends an email with the specified content.")]
        Task SendEmail([Description("The content of the email to send.")] string content);
    }
}
