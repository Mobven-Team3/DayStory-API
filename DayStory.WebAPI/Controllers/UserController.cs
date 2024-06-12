using Azure.Core;
using DayStory.Application.Interfaces;
using DayStory.Common.DTOs;
using DayStory.Common.DTOs.Users;
using DayStory.WebAPI.Helpers;
using DayStory.WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DayStory.WebAPI.Controllers;

[Route("api/[controller]s")]
[ApiController]
public class UserController : Controller
{
    private readonly IUserService _userService;
    public UserController(IUserService service)
    {
        _userService = service;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync(RegisterUserContract request)
    {
        await _userService.RegisterUserAsync(request);
        return Ok(new ResponseModel("Successfully Registered", StatusCodes.Status200OK));
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(LoginUserContract request)
    {
        var response = await _userService.LoginUserAsync(request);
        return Ok(new ResponseModel<LoginUserResponseContract>(response, StatusCodes.Status200OK));
    }

    [Authorize(Roles = "Admin, User")]
    [HttpPut]
    public async Task<IActionResult> UpdateAsync(UpdateUserContract request)
    {
        request.Id = int.Parse(JwtHelper.GetUserIdFromToken(HttpContext));
        await _userService.UpdateUserAsync(request);
        return Ok(new ResponseModel("Successfully Updated", StatusCodes.Status200OK));
    }

    [Authorize(Roles = "Admin, User")]
    [HttpPut("password")]
    public async Task<IActionResult> UpdatePasswordAsync(PasswordUpdateUserContract request)
    {
        request.Id = int.Parse(JwtHelper.GetUserIdFromToken(HttpContext));
        await _userService.UpdatePasswordAsync(request);
        return Ok(new ResponseModel("Successfully Updated", StatusCodes.Status200OK));
    }

    [Authorize(Roles = "Admin, User")]
    [HttpGet]
    public async Task<IActionResult> GetByIdAsync()
    {
        var id = int.Parse(JwtHelper.GetUserIdFromToken(HttpContext));
        var responseModel = await _userService.GetUserAsync(id);
        return Ok(new ResponseModel<GetUserContract>(responseModel, StatusCodes.Status200OK));
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _userService.RemoveByIdAsync(id);
        return Ok(new ResponseModel("Successfully Deleted", StatusCodes.Status200OK));
    }
}
