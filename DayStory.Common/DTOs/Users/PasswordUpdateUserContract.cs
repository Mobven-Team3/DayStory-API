namespace DayStory.Common.DTOs;
public class PasswordUpdateUserContract : IBaseContract
{
    public int Id { get; set; }
    public string CurrentPassword { get; set; }
    public string Password { get; set; }
    public string PasswordConfirmed { get; set; }
}
