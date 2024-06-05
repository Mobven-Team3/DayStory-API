namespace DayStory.Common.DTOs;

public class GetDaySummariesByMonthContract
{
    public string Year { get; set; }
    public string Month { get; set; }
    public int UserId { get; set; }
}
