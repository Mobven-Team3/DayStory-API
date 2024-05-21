using DayStory.Application.DTOs;
using DayStory.Application.Interfaces;
using DayStory.Application.Pagination;
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

    [HttpPost("Register")]
    public async Task<IActionResult> RegisterAsync(UserContract request)
    {
        await _userService.RegisterUserAsync(request);
        return Ok("Successfully Register");
    }

    [HttpPost("Login")]
    public async Task<IActionResult> LoginAsync(UserLoginContract request)
    {
        var response = await _userService.LoginUserAsync(request);
        return Ok(response);
    }

    [Authorize(Roles = "Admin, User")]
    [HttpPut]
    public async Task<IActionResult> UpdateAsync(UserContract request)
    {
        await _userService.UpdateAsync(request);
        return Ok("Updated");
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var responseModel = await _userService.GetAllAsync();
        return Ok(responseModel);
    }

    [Authorize(Roles = "Admin, User")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var responseModel = await _userService.GetByIdAsync(id);
        return Ok(responseModel);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _userService.RemoveByIdAsync(id);
        return Ok("Delete successful");
    }
}
