using System.ComponentModel;

namespace NewsSummarizer.Application.Services
{
    internal interface INewsService
    {
        [Description("Gets the latest news articles.")]
        Task<string> TriggerNewsFlow();
    }
}
