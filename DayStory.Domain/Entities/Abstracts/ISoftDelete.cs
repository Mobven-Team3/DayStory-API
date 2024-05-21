namespace DayStory.Domain.Entities;

public interface ISoftDelete
{
    public bool IsDeleted { get; set; } // default false
    public DateTime? DeletedOn { get; set; }
}
