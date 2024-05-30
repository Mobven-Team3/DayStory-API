using System.ComponentModel.DataAnnotations.Schema;

namespace DayStory.Domain.Entities;

[Table("ArtStyle", Schema = "DayStoryDb")]
public class ArtStyle : IBaseEntity, IAuditable, ISoftDelete
{
    public int Id { get; set; }
    public string Name { get; set; }

    public bool IsDeleted { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public DateTime? DeletedOn { get; set; }

    public virtual ICollection<DaySummary> DaySummaries { get; set; }
}
