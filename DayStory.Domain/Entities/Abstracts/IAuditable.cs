namespace DayStory.Domain.Entities;

public interface IAuditable
{
    public DateTime CreatedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }
}
