using DayStory.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace DayStory.Domain.Entities;

[Table("User", Schema = "DayStoryDB")]
public class User : IBaseEntity, IAuditable, ISoftDelete
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string HashedPassword { get; set; }
    public string BirthDate { get; set; }
    public string? ProfilePicturePath { get; set; }
    public Gender Gender { get; set; }

    public string Role { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public DateTime? DeletedOn { get; set; }
    public DateTime? LastLogin { get; set; }

    public virtual ICollection<Event>? Events { get; set; }
    public virtual ICollection<DaySummary>? DaySummaries { get; set; }
}
