using DayStory.Common.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace DayStory.Domain.Entities;

[Table("Event", Schema = "DayStoryDB")]
public class Event : IBaseEntity, IAuditable, ISoftDelete
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public string Date { get; set; }
    public string? Time { get; set; }
    public Priority Priority { get; set; }

    public bool IsDeleted { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public DateTime? DeletedOn { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }
    public int? DaySummaryId { get; set; }
    public DaySummary? DaySummary { get; set; }
}
