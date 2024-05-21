using System.ComponentModel.DataAnnotations.Schema;

namespace DayStory.Domain.Entities;

[Table("DaySummary", Schema = "DayStoryDB")]
public class DaySummary : IBaseEntity, IAuditable, ISoftDelete
{
    public int Id { get; set; }
    public string Date { get; set; }
    public string? ImagePath { get; set; }
    public string Summary { get; set; }

    public bool IsDeleted { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public DateTime? DeletedOn { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }
    public int? ArtStyleId { get; set; }
    public ArtStyle? ArtStyle { get; set; }
    public virtual ICollection<Event> Events { get; set; }
}
