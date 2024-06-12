namespace DayStory.Application.Interfaces;

public interface IOpenAIService
{
    Task<string> GetSummaryAsync(string text);
    Task<byte[]> GenerateImageAsync(string summary, string artStyle);
}
