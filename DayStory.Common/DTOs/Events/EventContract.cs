using DayStory.Common.Enums;

namespace DayStory.Common.DTOs;
public class EventContract : IBaseContract
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public string Date { get; set; }
    public string? Time { get; set; }
    public Priority Priority { get; set; }
}
