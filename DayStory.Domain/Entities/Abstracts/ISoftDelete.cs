namespace DayStory.Domain.Entities;

public interface ISoftDelete
{
    public bool IsDeleted { get; set; }
    public DateTime? DeletedOn { get; set; }
}
