using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
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
        var request = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/completions");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _openAIApiKey);

        var requestBody = new
        {
            model = "gpt-4",
            prompt = text,
            max_tokens = 100,
            temperature = 0.7
        };

        request.Content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        dynamic responseObject = JsonConvert.DeserializeObject(responseContent);

        return responseObject.choices[0].text;
    }

    public async Task<byte[]> GenerateImageAsync(string prompt)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/images/generations");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _openAIApiKey);

        var requestBody = new
        {
            prompt = prompt,
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
