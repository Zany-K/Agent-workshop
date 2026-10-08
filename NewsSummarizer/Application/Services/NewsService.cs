using System.Text;
using NewsSummarizer.Application.Model;
using NewsSummarizer.Application.Services;
using NewsSummarizer.Infrastructure.Http;

namespace NewsSummarizer.Application
{
    internal class NewsService : INewsService
    {
        private static readonly IHttpHandler httpHandler = new HttpHandler();

        private static string RSS_FEED_URI = "https://news.google.com/rss?hl=en-US&gl=US&ceid=US:en";

        public async Task<string> TriggerNewsFlow()
        {

            Console.WriteLine("Fetching news from RSS feed...");
            var feed = await httpHandler.FetchRssFeed(RSS_FEED_URI);

            if (feed == null)
            {
                Console.WriteLine("Failed to fetch RSS feed.");
                return string.Empty;
            }

            List<Article> articles = [];
            articles.AddRange(feed.Items.Select(item => new Article(
               item.Id,
               item.Title.Text,
               item.Summary.Text,
               item.Links[0].Uri.ToString(),
               item.Categories.Count > 0 ? item.Categories[0].Name : string.Empty,
               item.PublishDate.DateTime
            )));
            var allArticlesText = new StringBuilder();
            foreach (var article in articles)
            {
                allArticlesText.AppendLine($"Title: {article.Title}");
                allArticlesText.AppendLine($"Article: {article.Description}");
                allArticlesText.AppendLine($"Link: {article.Link}");
                allArticlesText.AppendLine();
            };

            return allArticlesText.ToString();
        }
    }
}
