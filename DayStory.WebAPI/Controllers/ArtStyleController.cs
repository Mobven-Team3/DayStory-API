using DayStory.Application.DTOs;
using DayStory.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DayStory.WebAPI.Controllers;

[Route("api/[controller]s")]
[ApiController]
public class ArtStyleController : Controller
{
    private readonly IArtStyleService _artStyleService;
    public ArtStyleController(IArtStyleService service)
    {
        _artStyleService = service;
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> CreateAsync(ArtStyleContract request)
    {
        await _artStyleService.AddAsync(request);
        return Ok();
    }

    [Authorize(Roles = "Admin")]
    [HttpPut]
    public async Task<IActionResult> UpdateAsync(ArtStyleContract request)
    {
        await _artStyleService.UpdateAsync(request);
        return Ok("Updated");
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var responseModel = await _artStyleService.GetAllAsync();
        return Ok(responseModel);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var responseModel = await _artStyleService.GetByIdAsync(id);
        return Ok(responseModel);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _artStyleService.RemoveByIdAsync(id);
        return Ok("Delete successful");
    }
}