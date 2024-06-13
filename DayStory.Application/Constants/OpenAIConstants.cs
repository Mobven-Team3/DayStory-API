namespace DayStory.Application;

public static class OpenAIConstants
{
    public const string ChatCompletionsEndpoint = "https://api.openai.com/v1/chat/completions";
    public const string ImageGenerationsEndpoint = "https://api.openai.com/v1/images/generations";
    public const string ChatModel = "gpt-3.5-turbo";
    public const string ImageModel = "dall-e-3";
    public const int MaxTokens = 150;
    public const double Temperature = 0.7;
    public const string ImageSize = "1024x1024";
    public const string ImageQuality = "standard";
    public const int ImageCount = 1;

    public const string AuthorizationHeaderScheme = "Bearer";
    public const string ContentTypeJson = "application/json";
    public const string SystemRole = "system";
    public const string UserRole = "user";
    public const string SystemMessageContent = "You are a helpful assistant.";
}
