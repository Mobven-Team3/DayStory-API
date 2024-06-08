namespace DayStory.Common.DTOs;

public class GetEventByIdRequestContract : IBaseContract
{
    public int Id { get; set; }
    public int UserId { get; set; }
}
