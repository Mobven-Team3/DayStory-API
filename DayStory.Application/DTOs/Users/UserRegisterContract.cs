﻿namespace DayStory.Application.DTOs;

public class UserRegisterContract : IBaseContract
{
    public int? Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string PasswordConfirmed { get; set; }
    public string BirthDate { get; set; }
    public string? ProfilePicturePath { get; set; }
    public string Gender { get; set; }
}
