namespace DayStory.Application.Constants;

public static class OpenAIPrompts
{
    public static string GetSummaryPrompt(string text)
    {
        return $"You are given a list of notes with titles and details that a user has added for a specific day. " +
            $"Your task is to create a single, cohesive paragraph that weaves all these notes together into a narrative. " +
            $"Ensure that the paragraph maintains a harmonious flow, integrating each note seamlessly into the story. " +
            $"/n {text} ; Now, create a single paragraph story that includes all these elements limited with 500 characters.";
    }

    public static string GetImagePrompt(string summary, string artStyle)
    {
        return $"Create an artistic image that visually represents the following narrative: {summary}. " +
            $"Do not include any text in the image." +
            $"Use the {artStyle} style, ensuring that all elements are harmoniously integrated and visually connected within the scene. " +
            $"The image should reflect a cohesive story where each part complements the others, forming a unified whole. " +
            $"Focus on creating a single, seamless artistic representation of the summarized day.";
    }
}
