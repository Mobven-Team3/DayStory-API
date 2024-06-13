namespace DayStory.Application.Helper;

public static class DaySummaryHelper
{
    public static string SaveImage(byte[] imageBytes, string date, int? userId)
    {
        var folderPath = Path.Combine("wwwroot", "images", "daysummaries", $"{userId}");
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        var fileName = $"{date.Replace("-", "")}_{Guid.NewGuid()}.png";
        var filePath = Path.Combine(folderPath, fileName);

        File.WriteAllBytes(filePath, imageBytes);

        return filePath;
    }

    public static string TrimSummaryToMaxLength(string summary, int maxLength)
    {
        return (summary.Length > maxLength) ? summary.Substring(0, maxLength - 3) + "..." : summary;
    }
}
