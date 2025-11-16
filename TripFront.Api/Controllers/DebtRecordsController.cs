using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TripFront.Api.Models;

namespace TripFront.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DebtRecordsController : ControllerBase
{
    private readonly IDebtRecordService _debtRecordService;

    public DebtRecordsController(IDebtRecordService debtRecordService)
    {
        _debtRecordService = debtRecordService;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<DebtRecord>> GetDebtRecord(int id)
    {
        var debtRecord = await _debtRecordService.Find(id);
        if (debtRecord == null)
        {
            return NotFound();
        }

        return Ok(debtRecord);
    }

    [HttpPost("trip/{tripId:int}/regenerate")]
    public async Task<ActionResult<List<DebtRecordDto>>> Regenerate(int tripId, [FromBody] RegenerateDebtRecordsRequest request)
    {
        if (request?.Debts == null)
        {
            return BadRequest("Debts are required.");
        }

        var records = await _debtRecordService.RegenerateDebtRecords(tripId, request.Debts);
        return Ok(records);
    }

    [HttpGet("{id:int}/receipt")]
    public async Task<ActionResult<string>> GetPaymentReceipt(int id)
    {
        var receipt = await _debtRecordService.GetPaymentReceipt(id);
        if (receipt == null)
        {
            return NotFound();
        }

        return Ok(receipt);
    }

    [HttpPut("{id:int}/receipt")]
    public async Task<IActionResult> UpdatePaymentReceipt(int id, [FromBody] PaymentReceiptRequest request)
    {
        await _debtRecordService.UpdatePaymentReceipt(id, request.Base64Receipt);
        return NoContent();
    }
}
