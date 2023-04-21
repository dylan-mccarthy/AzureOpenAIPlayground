using Azure;
using Azure.AI.OpenAI;
using Azure.Identity;
using System.IO;


string data = File.ReadAllText("Documentation.txt");

string initialMessage = "Please provide a summary of the following: \n\n Text: \n";
string endMessage = "\nSummary: ";

Console.WriteLine(initialMessage + data + endMessage);

string endpoint = "https://dm-openai-test-env.openai.azure.com/";

OpenAIClient client = new OpenAIClient(new Uri(endpoint), new DefaultAzureCredential());
var options = new CompletionsOptions();
options.Prompts.Add(initialMessage + data + endMessage);

var response = await client.GetCompletionsStreamingAsync(
    deploymentOrModelName: "gpt4-dm",
    options
);

using StreamingCompletions streamingCompletions = response.Value;

await foreach(StreamingChoice choice in streamingCompletions.GetChoicesStreaming())
{
    await foreach(var text in choice.GetTextStreaming()){
        Console.Write(text);
    }
    Console.WriteLine();
}
