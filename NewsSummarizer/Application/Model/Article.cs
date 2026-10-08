namespace NewsSummarizer.Application.Model
{
    internal record Article(string Id,
                            string Title,
                            string Description,
                            string Link,
                            string Category,
                            DateTime PublishedDate
                            )
    { }
}
