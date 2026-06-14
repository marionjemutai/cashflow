using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;
using CashflowGateway.Application;

namespace CashflowGateway.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class LedgerController : ControllerBase
{
    private readonly ILedgerService _ledgerService;

    public LedgerController(ILedgerService ledgerService)
    {
        _ledgerService = ledgerService;
    }

  
    [HttpGet]
    [Authorize(Roles = "ADMIN,MANAGER")]
    public async Task<IActionResult> GetLedger(
        [FromQuery] DateTime? from,
        [FromQuery] DateTime? to)
    {
        var entries = await _ledgerService.GetLedgerAsync(from, to);
        return Ok(entries);
    }

  

    [HttpGet("summary")]
    [Authorize(Roles = "ADMIN,MANAGER")]
    public async Task<IActionResult> GetSummary(
        [FromQuery] DateTime? from,
        [FromQuery] DateTime? to)
    {
        var summary = await _ledgerService.GetSummaryAsync(from, to);
        return Ok(summary);
    }
}