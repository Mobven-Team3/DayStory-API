using DayStory.Application.DTOs;
using DayStory.Application.Interfaces;
using DayStory.Application.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DayStory.WebAPI.Controllers;

public class EventController : Controller
{
    private readonly IEventService _eventService;
    public EventController(IEventService service)
    {
        _eventService = service;
    }

    [Authorize(Roles = "Admin, User")]
    [HttpPost]
    public async Task<IActionResult> CreateAsync(EventContract request)
    {
        await _eventService.AddAsync(request);
        return Ok();
    }

    [Authorize(Roles = "Admin, User")]
    [HttpPut]
    public async Task<IActionResult> UpdateAsync(EventContract request)
    {
        await _eventService.UpdateAsync(request);
        return Ok("Updated");
    }

    [Authorize(Roles = "Admin, User")]
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var responseModel = await _eventService.GetAllAsync();
        return Ok(responseModel);
    }

    [Authorize(Roles = "Admin, User")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var responseModel = await _eventService.GetByIdAsync(id);
        return Ok(responseModel);
    }

    [Authorize(Roles = "Admin, User")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _eventService.RemoveByIdAsync(id);
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
