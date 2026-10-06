using OllamaSharp;

namespace HelloWorld;

class Program
{
    // private const string MODEL_NAME = "hf.co/unsloth/Qwen3.5-9B-GGUF:Q4_K_M"; // Kan skapa en Modelfile för att justera namnet lokalt
    private const string MODEL_NAME = "qwen2.5:7b";
    private const string OLLAMA_LOCAL_URI = "http://localhost:11434";

     static async Task Main(string[] args)
    {
        var client = new OllamaApiClient(OLLAMA_LOCAL_URI, MODEL_NAME);
        var chat = new Chat(client);

        while (true)
        {
            Console.Write("You: ");
            
            var userPrompt = Console.ReadLine();
          
            Console.Write("Assistant: ");
            
            await foreach (var token in chat.SendAsync(userPrompt))
                Console.Write(token);

            Console.WriteLine();
            
        }
    }
}