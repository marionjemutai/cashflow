using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;
using CashflowGateway.Application;

namespace CashflowGateway.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ReportsController : ControllerBase
{
    private readonly IReportService _reportService;

    public ReportsController(IReportService reportService)
    {
        _reportService = reportService;
    }

    [HttpGet("daily")]
    [Authorize(Roles = "ADMIN,MANAGER")]
    public async Task<IActionResult> GetDailySales(
        [FromQuery] DateTime? from,
        [FromQuery] DateTime? to)
    {
        var result = await _reportService.GetDailySalesAsync(from, to);
        return Ok(result);
    }

    [HttpGet("products")]
    [Authorize(Roles = "ADMIN,MANAGER")]
    public async Task<IActionResult> GetTopProducts(
        [FromQuery] DateTime? from,
        [FromQuery] DateTime? to,
        [FromQuery] int limit = 10)
    {
        var result = await _reportService.GetTopProductsAsync(from, to, limit);
        return Ok(result);
    }

   
    [HttpGet("devices")]
    [Authorize(Roles = "ADMIN,MANAGER")]
    public async Task<IActionResult> GetDeviceSales(
        [FromQuery] DateTime? from,
        [FromQuery] DateTime? to)
    {
        var result = await _reportService.GetDeviceSalesAsync(from, to);
        return Ok(result);
    }
}