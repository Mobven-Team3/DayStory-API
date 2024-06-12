using System.Net.Http.Headers;
using System.Text;
using DayStory.Application.Constants;
using DayStory.Application.Interfaces;
using DayStory.Application.Options;
using DayStory.Common.DTOs;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace DayStory.Application.Services;

public class OpenAIService : IOpenAIService
{
    private readonly HttpClient _httpClient;
    private readonly OpenAIOptions _openAIOptions;

    public OpenAIService(HttpClient httpClient, IOptions<OpenAIOptions> openAIOptions)
    {
        _httpClient = httpClient;
        _openAIOptions = openAIOptions.Value;
    }

    public async Task<string> GetSummaryAsync(string text)
    {
        var request = CreateChatCompletionRequest(text);
        var responseContent = await SendRequestAsync(request);
        return ParseChatCompletionResponse(responseContent);
    }

    public async Task<byte[]> GenerateImageAsync(string summary, string artStyle)
    {
        var request = CreateImageGenerationRequest(summary, artStyle);
        var responseContent = await SendRequestAsync(request);
        return await ParseImageGenerationResponseAsync(responseContent);
    }

    #region Private Helper Methods

    private HttpRequestMessage CreateChatCompletionRequest(string text)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, _openAIOptions.ChatCompletionsEndpoint);
        request.Headers.Authorization = new AuthenticationHeaderValue(OpenAIConstants.AuthorizationHeaderScheme, _openAIOptions.ApiKey);

        var requestBody = new
        {
            model = OpenAIConstants.ChatModel,
            messages = new[]
            {
                    new { role = OpenAIConstants.SystemRole, content = OpenAIConstants.SystemMessageContent },
                    new { role = OpenAIConstants.UserRole, content = OpenAIPrompts.GetSummaryPrompt(text) }
                },
            max_tokens = OpenAIConstants.MaxTokens,
            temperature = OpenAIConstants.Temperature
        };

        request.Content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, OpenAIConstants.ContentTypeJson);
        return request;
    }

    private HttpRequestMessage CreateImageGenerationRequest(string summary, string artStyle)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, _openAIOptions.ImageGenerationsEndpoint);
        request.Headers.Authorization = new AuthenticationHeaderValue(OpenAIConstants.AuthorizationHeaderScheme, _openAIOptions.ApiKey);

        var requestBody = new
        {
            model = OpenAIConstants.ImageModel,
            prompt = OpenAIPrompts.GetImagePrompt(summary, artStyle),
            size = OpenAIConstants.ImageSize,
            quality = OpenAIConstants.ImageQuality,
            n = OpenAIConstants.ImageCount
        };

        request.Content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, OpenAIConstants.ContentTypeJson);
        return request;
    }

    private async Task<string> SendRequestAsync(HttpRequestMessage request)
    {
        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    private string ParseChatCompletionResponse(string responseContent)
    {
        var responseObject = JsonConvert.DeserializeObject<OpenAIResponse>(responseContent);

        return responseObject == null || responseObject.choices == null || responseObject.choices.Count == 0 || responseObject.choices[0].message == null
            ? throw new InvalidOperationException("Invalid response from OpenAI API.")
            : responseObject.choices[0].message.content;
    }

    private async Task<byte[]> ParseImageGenerationResponseAsync(string responseContent)
    {
        var responseObject = JsonConvert.DeserializeObject<OpenAIImageResponse>(responseContent);

        if (responseObject?.data == null || responseObject.data.Count == 0 || string.IsNullOrEmpty(responseObject.data[0].url))
        {
            throw new InvalidOperationException("Invalid response from OpenAI API.");
        }

        var imageUrl = responseObject.data[0].url;
        return await _httpClient.GetByteArrayAsync(imageUrl);
    }

    #endregion
}
