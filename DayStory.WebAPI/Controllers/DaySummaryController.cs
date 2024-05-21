using DayStory.Application.DTOs;
using DayStory.Application.Interfaces;
using DayStory.Application.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DayStory.WebAPI.Controllers;

[Route("api/[controller]s")]
[ApiController]
public class DaySummaryController : Controller
{
    private readonly IDaySummaryService _daySummaryService;
    public DaySummaryController(IDaySummaryService service)
    {
        _daySummaryService = service;
    }

    [Authorize(Roles = "Admin, User")]
    [HttpPost]
    public async Task<IActionResult> CreateAsync(DaySummaryContract request)
    {
        await _daySummaryService.AddAsync(request);
        return Ok();
    }

    [Authorize(Roles = "Admin, User")]
    [HttpPut]
    public async Task<IActionResult> UpdateAsync(DaySummaryContract request)
    {
        await _daySummaryService.UpdateAsync(request);
        return Ok("Updated");
    }

    [Authorize(Roles = "Admin, User")]
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var responseModel = await _daySummaryService.GetAllAsync();
        return Ok(responseModel);
    }

    [Authorize(Roles = "Admin, User")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var responseModel = await _daySummaryService.GetByIdAsync(id);
        return Ok(responseModel);
    }

    [Authorize(Roles = "Admin, User")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _daySummaryService.RemoveByIdAsync(id);
        return Ok("Delete successful");
    }

    [Authorize(Roles = "Admin, User")]
    [HttpGet("Pages")]
    public async Task<IActionResult> GetAllPagedAsync([FromQuery] PaginationFilter filter)
    {
        PaginationFilter paginationFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
        var responseModel = await _daySummaryService.GetPagedDataAsync(paginationFilter.PageNumber, paginationFilter.PageSize);
        return Ok(responseModel);
    }
}
