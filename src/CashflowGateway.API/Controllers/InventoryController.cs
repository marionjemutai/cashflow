using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;
using CashflowGateway.Application;

namespace CashflowGateway.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class InventoryController : ControllerBase
{
    private readonly IInventoryService _inventoryService;

    public InventoryController(IInventoryService inventoryService)
    {
        _inventoryService = inventoryService;
    }

  
    [HttpGet]
    [Authorize(Roles = "ADMIN,MANAGER,CASHIER")]
    public async Task<IActionResult> GetProducts([FromQuery] Guid? storeId)
    {
        var products = await _inventoryService.GetProductsAsync(storeId);
        return Ok(products);
    }

  
    [HttpGet("{id}")]
    [Authorize(Roles = "ADMIN,MANAGER,CASHIER")]
    public async Task<IActionResult> GetProduct(Guid id)
    {
        var product = await _inventoryService.GetProductByIdAsync(id);
        if (product == null) return NotFound("Product not found.");
        return Ok(product);
    }

 
    [HttpPost]
    [Authorize(Roles = "ADMIN,MANAGER")]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            return BadRequest("Product name is required.");

        if (request.Price <= 0)
            return BadRequest("Price must be greater than zero.");

        var product = await _inventoryService.CreateProductAsync(request);
        return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
    }


    [HttpPut("{id}")]
    [Authorize(Roles = "ADMIN,MANAGER")]
    public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] UpdateProductDto request)
    {
        var product = await _inventoryService.UpdateProductAsync(id, request);
        if (product == null) return NotFound("Product not found.");
        return Ok(product);
    }

  
    [HttpDelete("{id}")]
    [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> DeactivateProduct(Guid id)
    {
        var result = await _inventoryService.DeactivateProductAsync(id);
        if (!result) return NotFound("Product not found.");
        return Ok(new { success = true, message = "Product deactivated." });
    }

    
    [HttpGet("{id}/movements")]
    [Authorize(Roles = "ADMIN,MANAGER")]
    public async Task<IActionResult> GetMovements(Guid id)
    {
        var movements = await _inventoryService.GetMovementsAsync(id);
        return Ok(movements);
    }
}