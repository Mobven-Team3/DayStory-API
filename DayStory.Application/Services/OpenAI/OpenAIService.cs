using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using DayStory.Domain.Entities;
using Newtonsoft.Json;

namespace DayStory.Application.Services;

public class OpenAIService
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
            new { role = "user", content = $"Özetle (Türkçe, maksimum 500 karakter):\n{text}" }
        },
            max_tokens = 150, // Yaklaşık 500 karakter
            temperature = 0.7
        };

        request.Content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        dynamic responseObject = JsonConvert.DeserializeObject(responseContent);

        return responseObject.choices[0].message.content;
    }

    public async Task<byte[]> GenerateImageAsync(string summary, string artStyle)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/images/generations");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _openAIApiKey);

        var requestBody = new
        {
            prompt = $"Create an image in {artStyle} style based on the following summary: {summary}",
            n = 1,
            size = "512x512"
        };

        request.Content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        dynamic responseObject = JsonConvert.DeserializeObject(responseContent);

        var imageUrl = responseObject.data[0].url;
        return await _httpClient.GetByteArrayAsync(imageUrl);
    }
}
