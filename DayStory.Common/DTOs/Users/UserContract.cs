using DayStory.Common.Enums;

namespace DayStory.Common.DTOs;
public class UserContract : IBaseContract
{
    public int? Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string BirthDate { get; set; }
    public Gender Gender { get; set; }
}
