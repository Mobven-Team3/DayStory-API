namespace DayStory.Common.DTOs;

public class GetDaySummaryContract : IBaseContract
{
    public int Id { get; set; }
    public string Date { get; set; }
    public string ImagePath { get; set; }
    //public List<GetEventContract> Events { get; set; }
}
