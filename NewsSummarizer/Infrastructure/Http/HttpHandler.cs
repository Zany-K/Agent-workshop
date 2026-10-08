using System.ServiceModel.Syndication;
using System.Xml;

namespace NewsSummarizer.Infrastructure.Http
{
    internal class HttpHandler : IHttpHandler
    {
        private static readonly HttpClient httpClient = new();

        public HttpHandler()
        {
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36");
        }

        public async Task<SyndicationFeed?> FetchRssFeed(string url)
        {
            try
            {

                using var response = await httpClient.GetAsync(url);
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    Console.WriteLine($"Failed to fetch RSS feed. Status code: {response.StatusCode}");
                    return null;
                }

                using var responseStream = await response.Content.ReadAsStreamAsync();

                using var reader = XmlReader.Create(responseStream);
                return SyndicationFeed.Load(reader);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching RSS feed: {ex.Message}");
                throw;
            }
        }

        public async Task<string> FetchContent(string url)
        {
            try
            {
                var response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching content from {url}: {ex.Message}");
                throw;
            }
        }
    }
}
