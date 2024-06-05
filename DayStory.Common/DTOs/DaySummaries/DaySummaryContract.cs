namespace DayStory.Common.DTOs;
public class DaySummaryContract : IBaseContract
{
    public int Id { get; set; }
    public string Date { get; set; }
    public string? ImagePath { get; set; } // AI
    public string Summary { get; set; } // AI
    public List<GetEventContract> Events { get; set; }
    public int UserId { get; set; }
    public int ArtStyleId { get; set; }
}
