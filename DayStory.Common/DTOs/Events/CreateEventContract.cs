namespace DayStory.Common.DTOs;

public class CreateEventContract : IBaseContract
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public string Date { get; set; }
    public string? Time { get; set; }
    public string? Priority { get; set; }
    public int UserId { get; set; }
}
