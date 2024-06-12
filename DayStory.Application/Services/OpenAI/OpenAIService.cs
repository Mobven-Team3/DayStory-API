using System.Net.Http.Headers;
using System.Text;
using DayStory.Application.Constants;
using DayStory.Application.Interfaces;
using DayStory.Common.DTOs;
using Newtonsoft.Json;

namespace DayStory.Application.Services;

public class OpenAIService : IOpenAIService
{
    private readonly HttpClient _httpClient;
    private readonly string _openAIApiKey;

    public OpenAIService(HttpClient httpClient, string openAIApiKey)
    {
        _httpClient = httpClient;
        _openAIApiKey = openAIApiKey;
    }

    public async Task<string> GetSummaryAsync(string text)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/chat/completions");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _openAIApiKey);

        var requestBody = new
        {
            model = "gpt-3.5-turbo",
            messages = new[]
            {
                new { role = "system", content = "You are a helpful assistant." },
                new { role = "user", content = OpenAIPrompts.GetSummaryPrompt(text)}
            },
            max_tokens = 150,
            temperature = 0.7
        };

        request.Content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        var responseObject = JsonConvert.DeserializeObject<OpenAIResponse>(responseContent);

        if (responseObject == null || responseObject.choices == null || responseObject.choices.Count == 0 || responseObject.choices[0].message == null)
        {
            throw new InvalidOperationException("Invalid response from OpenAI API.");
        }

        return responseObject.choices[0].message.content;
    }

    public async Task<byte[]> GenerateImageAsync(string summary, string artStyle)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/images/generations");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _openAIApiKey);

        var requestBody = new
        {
            model = "dall-e-3",
            prompt = OpenAIPrompts.GetImagePrompt(summary, artStyle),
            size = "1024x1024",
            quality = "standard",
            n = 1
        };

        request.Content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

        var response = await _httpClient.SendAsync(request);
        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Failed to generate image: {response.StatusCode} {errorContent}");
        }

        var responseContent = await response.Content.ReadAsStringAsync();
        var responseObject = JsonConvert.DeserializeObject<OpenAIImageResponse>(responseContent);

        if (responseObject?.data == null || responseObject.data.Count == 0 || string.IsNullOrEmpty(responseObject.data[0].url))
        {
            throw new InvalidOperationException("Invalid response from OpenAI API.");
        }

        var imageUrl = responseObject.data[0].url;
        return await _httpClient.GetByteArrayAsync(imageUrl);
    }
}
