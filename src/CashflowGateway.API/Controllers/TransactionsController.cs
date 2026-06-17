using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;
using CashflowGateway.Application;

namespace CashflowGateway.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TransactionsController : ControllerBase
{
    private readonly IReceiptService _receiptService;

    public TransactionsController(IReceiptService receiptService)
    {
        _receiptService = receiptService;
    }
    [HttpGet("{id}/receipt")]
    [Authorize(Roles = "ADMIN,MANAGER,CASHIER")]
    public async Task<IActionResult> GetReceipt(Guid id)
    {
        var receipt = await _receiptService.GetReceiptAsync(id);
        if (receipt == null) return NotFound("Transaction not found.");
        return Ok(receipt);
    }
}