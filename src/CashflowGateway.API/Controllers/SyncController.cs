using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using CashflowGateway.Application;

namespace CashflowGateway.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]  
public class SyncController : ControllerBase
{
    private readonly ISyncService _syncService;
    

    public SyncController(ISyncService syncService)
    {
        _syncService = syncService;
    }


    [HttpPost("payload")]
    [Authorize(Roles = "CASHIER,MANAGER,ADMIN")]
    public async Task<IActionResult> SyncPayload([FromBody] SyncPayloadDto payload)
    {
        if (payload == null)
            return BadRequest("Payload cannot be empty.");

        var result = await _syncService.ProcessSyncPayloadAsync(payload);

        if (result)
            return Ok(new { success = true, serverTime = DateTime.UtcNow });

        return StatusCode(500, "Sync engine encountered a processing error.");
    }

    [HttpGet("pull")]
    [Authorize(Roles = "CASHIER,MANAGER,ADMIN")]
    public async Task<IActionResult> PullData(
        [FromQuery] Guid deviceId,
        [FromQuery] DateTime since)
    {
        if (deviceId == Guid.Empty)
            return BadRequest("A valid deviceId is required.");

        var result = await _syncService.GetPullDataAsync(deviceId, since);
        return Ok(result);
    }
  
    private Guid GetCurrentUserId()
    {
        var sub = User.FindFirstValue(System.Security.Claims.ClaimTypes.NameIdentifier);
        return Guid.TryParse(sub, out var id) ? id : Guid.Empty;
    }

  
    private string GetCurrentUserRole()
    {
        return User.FindFirstValue(System.Security.Claims.ClaimTypes.Role) ?? string.Empty;
    }
}