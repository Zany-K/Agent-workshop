using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using NewsSummarizer.Application;
using NewsSummarizer.Application.Services;
using NewsSummarizer.Infrastructure.Ollama;
using NewsSummarizer.Infrastructure.Smtp;
using OllamaSharp;

namespace NewsDigest
{
    internal class Program
    {
        private static ISmtpHandler _smtpHandler = new SmtpHandler(
            new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddUserSecrets<Program>()
                .Build());
        private static INewsService _newsService = new NewsService();
        private static readonly IOllamaApiHandler _ollamaApiHandler = new OllamaApiHandler();

        private const string MODEL_NAME = "hf.co/unsloth/Qwen3.5-9B-GGUF:Q4_K_M";

        static async Task Main(string[] args)
        {
            var tools = new List<AITool>
            {
                AIFunctionFactory.Create(_newsService.TriggerNewsFlow),
                AIFunctionFactory.Create(_smtpHandler.SendEmail)
            };

            AIAgent agent = new OllamaApiClient("http://localhost:11434", MODEL_NAME)
                .AsAIAgent(
                    instructions:
                        "You are a news summarizer. Use the news fetched through the tool as the only source of truth. " +
                        "Write a short, polished HTML email digest, Company names from stories should be highlighted." +
                        "Group by topic, keep it concise, and include the top stories." +
                        "If the user asks for a summary, respond in HTML. " +
                        "If they ask for a summary of the news, use the news tool. " +
                        "If they ask to send email, use the email tool.",
                    tools: tools);

            var session = await agent.CreateSessionAsync();

            while (true)
            {
                Console.Write("You > ");
                var input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input)) continue;
                if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
                {
                    _ollamaApiHandler.UnloadModel(MODEL_NAME);
                    break;
                }

                Console.Write("Agent > ");
                await foreach (var update in agent.RunStreamingAsync(input, session))
                {
                    if (update is not null)
                    {
                        Console.Write(update);
                    }
                }

                Console.WriteLine();
            }
        }
    }
}