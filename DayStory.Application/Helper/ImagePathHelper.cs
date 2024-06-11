namespace DayStory.Application.Helper;

public static class ImagePathHelper
{
    public static string RemovePathPrefix(string imagePath)
    {
        imagePath = (imagePath.StartsWith("wwwroot")) ? imagePath.Substring("wwwroot".Length) : imagePath;
        imagePath = imagePath.Replace("\\", "/");
        return imagePath;
    }
}
