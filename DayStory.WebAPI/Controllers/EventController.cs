using DayStory.Application.Interfaces;
using DayStory.Application.Pagination;
using DayStory.Common.DTOs;
using DayStory.WebAPI.Helpers;
using DayStory.WebAPI.Models;
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
    public async Task<IActionResult> CreateAsync(CreateEventContract request)
    {
        request.UserId = int.Parse(JwtHelper.GetUserIdFromToken(HttpContext));
        await _eventService.AddEventAsync(request);
        return Ok(new ResponseModel("Successfully Created", StatusCodes.Status200OK));
    }

    [Authorize(Roles = "Admin, User")]
    [HttpPut]
    public async Task<IActionResult> UpdateAsync(UpdateEventContract request)
    {
        request.UserId = int.Parse(JwtHelper.GetUserIdFromToken(HttpContext));
        await _eventService.UpdateEventAsync(request);
        return Ok(new ResponseModel("Successfully Updated", StatusCodes.Status200OK));
    }

    [Authorize(Roles = "Admin, User")]
    [HttpGet("all")]
    public async Task<IActionResult> GetAllAsync()
    {
        var userId = int.Parse(JwtHelper.GetUserIdFromToken(HttpContext));
        var responseModel = await _eventService.GetEventsAsync(userId);
        return Ok(new ResponseModel<List<GetEventContract>>(responseModel, StatusCodes.Status200OK));
    }

    [Authorize(Roles = "Admin, User")]
    [HttpGet("{Id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var userId = int.Parse(JwtHelper.GetUserIdFromToken(HttpContext));
        var responseModel = await _eventService.GetEventByIdAsync(id, userId);
        return Ok(new ResponseModel<GetEventContract>(responseModel, StatusCodes.Status200OK));
    }

    [Authorize(Roles = "Admin, User")]
    [HttpGet("day")]    // /api/events/day?date=05-06-2023
    public async Task<IActionResult> GetEventsByDayAsync([FromQuery] GetEventsByDayContract request)
    {
        request.UserId = int.Parse(JwtHelper.GetUserIdFromToken(HttpContext));
        var responseModel = await _eventService.GetEventsByDayAsync(request);
        return Ok(new ResponseModel<List<GetEventContract>>(responseModel, StatusCodes.Status200OK));
    }

    [Authorize(Roles = "Admin, User")]
    [HttpGet("month")]  ///api/events/month?year=2023&month=6
    public async Task<IActionResult> GetEventsByMonthAsync([FromQuery] GetEventsByMonthContract request)
    {
        request.UserId = int.Parse(JwtHelper.GetUserIdFromToken(HttpContext));
        var responseModel = await _eventService.GetEventsByMonthAsync(request);
        return Ok(new ResponseModel<List<GetEventContract>>(responseModel, StatusCodes.Status200OK));
    }

    [Authorize(Roles = "Admin, User")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var userId = int.Parse(JwtHelper.GetUserIdFromToken(HttpContext));
        await _eventService.RemoveEventByIdAsync(id, userId);
        return Ok(new ResponseModel("Successfully Deleted", StatusCodes.Status200OK));
    }

    [Authorize(Roles = "Admin, User")]
    [HttpGet("pages")]
    public async Task<IActionResult> GetAllPagedAsync([FromQuery] PaginationFilter filter)
    {
        PaginationFilter paginationFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
        var responseModel = await _eventService.GetPagedDataAsync(paginationFilter.PageNumber, paginationFilter.PageSize);
        return Ok(responseModel);
    }
}
