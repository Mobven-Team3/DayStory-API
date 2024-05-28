using DayStory.Application.Interfaces;
using DayStory.Application.Pagination;
using DayStory.Common.DTOs;
using DayStory.WebAPI.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DayStory.WebAPI.Controllers;

[Route("api/[controller]s")]
[ApiController]
public class EventController : Controller
{
    private readonly IEventService _eventService;
    public EventController(IEventService service)
    {
        _eventService = service;
    }

    [Authorize(Roles = "Admin, User")]
    [HttpPost]
    public async Task<IActionResult> CreateAsync(EventCreateContract request)
    {
        await _eventService.AddEventAsync(request);
        return Ok();
    }

    [Authorize(Roles = "Admin, User")]
    [HttpPut]
    public async Task<IActionResult> UpdateAsync(EventUpdateContract request)
    {
        await _eventService.UpdateEventAsync(request);
        return Ok("Updated");
    }

    [Authorize(Roles = "Admin, User")]
    [HttpGet("get-all/{userId}")]
    public async Task<IActionResult> GetAllAsync(int userId)
    {
        var responseModel = await _eventService.GetAllEventAsync(userId);
        return Ok(responseModel);
    }

    [Authorize(Roles = "Admin, User")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var responseModel = await _eventService.GetEventByIdAsync(id);
        return Ok(responseModel);
    }

    [Authorize(Roles = "Admin, User")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _eventService.RemoveEventByIdAsync(id);
        return Ok("Delete successful");
    }

    [Authorize(Roles = "Admin, User")]
    [HttpGet("Pages")]
    public async Task<IActionResult> GetAllPagedAsync([FromQuery] PaginationFilter filter)
    {
        PaginationFilter paginationFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
        var responseModel = await _eventService.GetPagedDataAsync(paginationFilter.PageNumber, paginationFilter.PageSize);
        return Ok(responseModel);
    }
}
