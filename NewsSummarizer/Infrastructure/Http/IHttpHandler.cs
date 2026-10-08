using System.ServiceModel.Syndication;

namespace NewsSummarizer.Infrastructure.Http
{
    internal interface IHttpHandler
    {
        Task<SyndicationFeed?> FetchRssFeed(string url);

        Task <string> FetchContent(string url);
    }
}
