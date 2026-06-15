using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;
using CashflowGateway.Application;

namespace CashflowGateway.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DevicesController : ControllerBase
{
    private readonly IDeviceService _deviceService;

    public DevicesController(IDeviceService deviceService)
    {
        _deviceService = deviceService;
    }

    [HttpPost("register")]
    [Authorize(Roles = "ADMIN,MANAGER")]
    public async Task<IActionResult> Register([FromBody] RegisterDeviceDto request)
    {
        if (string.IsNullOrWhiteSpace(request.DeviceName))
            return BadRequest("Device name is required.");

        if (string.IsNullOrWhiteSpace(request.DeviceKey))
            return BadRequest("Device key is required.");

        var device = await _deviceService.RegisterAsync(request);
        return CreatedAtAction(nameof(GetAll), device);
    }

    [HttpGet]
    [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> GetAll()
    {
        var devices = await _deviceService.GetAllAsync();
        return Ok(devices);
    }

    [HttpPut("{id}/ping")]
    [Authorize(Roles = "ADMIN,MANAGER,CASHIER")]
    public async Task<IActionResult> Ping(Guid id)
    {
        var device = await _deviceService.PingAsync(id);
        if (device == null) return NotFound("Device not found.");
        return Ok(device);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> Deactivate(Guid id)
    {
        var result = await _deviceService.DeactivateAsync(id);
        if (!result) return NotFound("Device not found.");
        return Ok(new { success = true, message = "Device deactivated." });
    }
}