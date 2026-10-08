using FirstAgent.Tools;
using Microsoft.Extensions.AI;
using OllamaSharp;
using ChatRole = Microsoft.Extensions.AI.ChatRole;

namespace FirstAgent;

class Program
{
    // private const string MODEL_NAME = "hf.co/unsloth/Qwen3.5-9B-GGUF:Q4_K_M"; // Kan skapa en Modelfile för att justera namnet lokalt
    private const string MODEL_NAME = "qwen2.5:7b";
    private const string OLLAMA_LOCAL_URI = "http://localhost:11434";
    private static readonly IXTool XTool = new XTool();

    static async Task Main(string[] args)
    {
        IChatClient client = new OllamaApiClient(OLLAMA_LOCAL_URI, MODEL_NAME);
        
        var chatClient = new ChatClientBuilder(client)
            .UseFunctionInvocation()
            .Build();

        var options = new ChatOptions
        {
            Tools =
            [
                AIFunctionFactory.Create(XTool.GetY),
                AIFunctionFactory.Create(XTool.GetZ)
            ]
        };

        var messages = new List<ChatMessage>()
        {
            new (ChatRole.System, content: "You are a helpful assistant." +
                                           "Use the tools that are available to you instead of guessing.")
        };
        
        while (true)
        {
            Console.Write("You: ");
            
            var userPrompt = Console.ReadLine();
            
            messages.Add(new ChatMessage(ChatRole.User, userPrompt));
          
            Console.Write("Assistant: ");

            var updates = new List<ChatResponseUpdate>();
            await foreach (var token in chatClient.GetStreamingResponseAsync(messages, options))
            {
                updates.Add(token);
                Console.Write(token);
            }
            
            messages.AddRange(updates.ToChatResponse().Messages);

            Console.WriteLine();
        }
    }
}