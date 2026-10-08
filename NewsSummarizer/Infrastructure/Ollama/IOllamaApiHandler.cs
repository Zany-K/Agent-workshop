namespace NewsSummarizer.Infrastructure.Ollama
{
    internal interface IOllamaApiHandler
    {
        void UnloadModel(string modelName);
    }
}
