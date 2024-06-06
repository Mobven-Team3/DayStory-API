using DayStory.Application.Interfaces;
using DayStory.Common.DTOs;
using DayStory.WebAPI.Helpers;
using DayStory.WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DayStory.WebAPI.Controllers;

[Route("api/[controller]s")]
[ApiController]
public class DaySummaryController : Controller
{
    private readonly IDaySummaryService _daySummaryService;
    public DaySummaryController(IDaySummaryService service, IConfiguration configuration)
    {
        _daySummaryService = service;
    }

    [Authorize(Roles = "Admin, User")]
    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateDaySummaryContract request)
    {
        request.UserId = int.Parse(JwtHelper.GetUserIdFromToken(HttpContext));
        await _daySummaryService.AddDaySummaryAsync(request);
        return Ok(new ResponseModel("Successfully Created"));
    }

    //[Authorize(Roles = "Admin, User")]
    //[HttpPut]
    //public async Task<IActionResult> UpdateAsync(DaySummaryContract request)
    //{
    //    await _daySummaryService.UpdateAsync(request);
    //    return Ok(new ResponseModel("Successfully Updated"));
    //}

    [Authorize(Roles = "Admin, User")]
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var userId = int.Parse(JwtHelper.GetUserIdFromToken(HttpContext));
        var responseModel = await _daySummaryService.GetDaySummariesAsync(userId);
        return Ok(new ResponseModel<List<GetDaySummaryContract>>(responseModel));
    }

    [Authorize(Roles = "Admin, User")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var responseModel = await _daySummaryService.GetByIdAsync(id);
        return Ok(new ResponseModel<DaySummaryContract>(responseModel));
    }

    [Authorize(Roles = "Admin, User")]
    [HttpGet("day")]    // /api/daysummarys/day?date=05-06-2023
    public async Task<IActionResult> GetDaySummaryByDayAsync([FromQuery] GetDaySummaryByDayContract request)
    {
        request.UserId = int.Parse(JwtHelper.GetUserIdFromToken(HttpContext));
        var responseModel = await _daySummaryService.GetDaySummaryByDayAsync(request);
        return Ok(new ResponseModel<GetDaySummaryContract>(responseModel));
    }

    [Authorize(Roles = "Admin, User")]
    [HttpGet("month")]  // /api/daysummarys/day?date=05-06-2023
    public async Task<IActionResult> GetDaySummariesByMonthAsync([FromQuery] GetDaySummariesByMonthContract request)
    {
        request.UserId = int.Parse(JwtHelper.GetUserIdFromToken(HttpContext));
        var responseModel = await _daySummaryService.GetDaySummariesByMonthAsync(request);
        return Ok(new ResponseModel<List<GetDaySummaryContract>>(responseModel));
    }

    //[Authorize(Roles = "Admin, User")]
    //[HttpDelete("{id}")]
    //public async Task<IActionResult> DeleteAsync(int id)
    //{
    //    await _daySummaryService.RemoveByIdAsync(id);
    //    return Ok(new ResponseModel("Successfully Deleted"));
    //}

    //[Authorize(Roles = "Admin, User")]
    //[HttpGet("Pages")]
    //public async Task<IActionResult> GetAllPagedAsync([FromQuery] PaginationFilter filter)
    //{
    //    PaginationFilter paginationFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
    //    var responseModel = await _daySummaryService.GetPagedDataAsync(paginationFilter.PageNumber, paginationFilter.PageSize);
    //    return Ok(responseModel);
    //}
}
