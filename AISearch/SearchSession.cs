using Azure;
using Azure.AI.OpenAI;

namespace AISearch;

// This class is used to create a session for the user to search for products
// The session should be able to be started with an initial message for the system to pass to the OpenAI model
// The session should then be able to have messages passed in from the user and a response returned
// The session should be able to be ended

public class SearchSession {
    private readonly string _initialMessage;
    private bool _isEnded;
    private readonly OpenAIClient _client;

    public SearchSession(string initialMessage, OpenAIClient client) {
        _initialMessage = initialMessage;
        _client = client;
    }

    public string ProcessMessage(string message)
    {
        if (_isEnded)
        {
            throw new InvalidOperationException("Session has already ended.");
        }

        // Pass message to OpenAI model and get response
        string response = GetResponseFromOpenAI(message);

        // Check if session should be ended
        if (ShouldEndSession(response))
        {
            _isEnded = true;
        }

        return response;
    }
    public bool IsEnded()
    {
        return _isEnded;
    }

    public string GetResponseFromOpenAI(string message)
    {
        var chatCompletetionOptions = new ChatCompletionsOptions()
        {
            Messages = {
                new ChatMessage(ChatRole.System, _initialMessage),
                new ChatMessage(ChatRole.User, message),
            }
        };

        Response<ChatCompletions> chatCompletions = _client.GetChatCompletions("gpt3-5", chatCompletetionOptions);

        return chatCompletions.Value.Choices[0].Message.Content;
    }

    private bool ShouldEndSession(string response)
    {
        return false;
    }

}