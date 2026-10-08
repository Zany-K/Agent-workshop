using OllamaSharp;

namespace NewsSummarizer.Infrastructure.Ollama
{
    internal class OllamaApiHandler : IOllamaApiHandler
    {

        public async void UnloadModel(string modelName)
        {
            var ollama = new OllamaApiClient("http://localhost:11434");
            await ollama.RequestModelUnloadAsync(modelName);
        }
    }
}
