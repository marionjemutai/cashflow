using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;
using CashflowGateway.Application;

namespace CashflowGateway.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class StoresController : ControllerBase
{
    private readonly IStoreService _storeService;

    public StoresController(IStoreService storeService)
    {
        _storeService = storeService;
    }
    [HttpGet]
    [Authorize(Roles = "ADMIN,MANAGER,CASHIER")]
    public async Task<IActionResult> GetAll()
    {
        var stores = await _storeService.GetAllAsync();
        return Ok(stores);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "ADMIN,MANAGER,CASHIER")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var store = await _storeService.GetByIdAsync(id);
        if (store == null) return NotFound("Store not found.");
        return Ok(store);
    }

    [HttpPost]
    [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> Create([FromBody] CreateStoreDto request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            return BadRequest("Store name is required.");

        var store = await _storeService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = store.Id }, store);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateStoreDto request)
    {
        var store = await _storeService.UpdateAsync(id, request);
        if (store == null) return NotFound("Store not found.");
        return Ok(store);
    }
}