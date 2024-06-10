namespace DayStory.Application.Constants;

public static class OpenAIPrompts
{
    public static string GetSummaryPrompt(string text)
    {
        return $"Günü hikayeleştirerek özetle (Türkçe, maksimum 500 karakter):\n{text}";
    }

    public static string GetImagePrompt(string summary, string artStyle)
    {
        return $"{artStyle} temasında özeti: {summary} baz alarak hikayeleştirerek tek bir fotoğraf karesi oluştur.";
    }
}
