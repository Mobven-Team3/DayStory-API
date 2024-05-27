namespace DayStory.Common.DTOs;
public class DaySummaryContract : IBaseContract
{
    public int? Id { get; set; }
    public string Date { get; set; }
    public string? ImagePath { get; set; }
    public string Summary { get; set; }
    public List<int> EventIds { get; set; }
    public int UserId { get; set; }
}
