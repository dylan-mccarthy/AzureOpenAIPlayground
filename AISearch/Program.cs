using Azure;
using Azure.AI.OpenAI;
using Azure.Identity;
using System.IO;
using Newtonsoft.Json;

namespace AISearch;

class Program
{
    static void Main()
    {
        string endpoint = "https://dm-openai-test-env.openai.azure.com/";

        OpenAIClient client = new OpenAIClient(new Uri(endpoint), new DefaultAzureCredential());

        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Products.json");
        string json = File.ReadAllText(filePath);
        // replace all new lines with spaces
        json = json.Replace("\r\n", " ");

        var initialMessage = $"You are a product search AI and answers questions on the following data: ${json}";
        
        SearchSession session = new SearchSession(initialMessage, client);

        while(!session.IsEnded())
        {
            Console.WriteLine("Enter a message:");
            string message = Console.ReadLine();
            string response = session.ProcessMessage(message);
            Console.WriteLine(response);
        }
        
    }
}

