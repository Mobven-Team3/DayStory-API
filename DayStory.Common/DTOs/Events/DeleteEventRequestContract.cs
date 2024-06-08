namespace DayStory.Common.DTOs;

public class DeleteEventRequestContract : IBaseContract
{
    public int Id { get; set; }
    public int UserId { get; set; }
}
