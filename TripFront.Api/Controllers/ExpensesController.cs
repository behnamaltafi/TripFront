using FilterPagingEfCore.Paging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TripFront.Api.Models;

namespace TripFront.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ExpensesController : ControllerBase
{
    private readonly IExpenseService _expenseService;

    public ExpensesController(IExpenseService expenseService)
    {
        _expenseService = expenseService;
    }

    [HttpGet("trip/{tripId:int}")]
    public async Task<ActionResult<PagingResult<Expense>>> GetTripExpenses(int tripId, [FromQuery] PagingRequest paging)
    {
        var result = await _expenseService.FindTripExpenses(paging.ToPagingParam(), tripId);
        return Ok(result);
    }

    [HttpGet("trip/{tripId:int}/family/{familyId:int}")]
    public async Task<ActionResult<List<ExpenseDTO>>> GetFamilyExpenses(int tripId, int familyId)
    {
        var expenses = await _expenseService.FindFamilyExpenses(tripId, familyId);
        return Ok(expenses);
    }

    [HttpPost]
    public async Task<ActionResult<ExpenseDTO>> CreateExpense([FromBody] AddExpenseDTO expenseDto)
    {
        var expense = await _expenseService.Add(expenseDto);
        return CreatedAtAction(nameof(GetTripExpenses), new { tripId = expenseDto.TripId }, expense);
    }

    [HttpPut("{expenseId:int}")]
    public async Task<IActionResult> UpdateExpense(int expenseId, [FromBody] UpdateExpenseDTO expenseDto)
    {
        if (expenseId != expenseDto.Id)
        {
            return BadRequest("Expense id mismatch.");
        }

        await _expenseService.Update(expenseDto);
        return NoContent();
    }

    [HttpDelete("{expenseId:int}")]
    public async Task<IActionResult> DeleteExpense(int expenseId)
    {
        await _expenseService.Remove(expenseId);
        return NoContent();
    }

    [HttpPut("{expenseId:int}/participants/{familyId:int}")]
    public async Task<IActionResult> UpdateParticipant(int expenseId, int familyId, [FromBody] UpdateParticipantDto participantDto)
    {
        await _expenseService.UpdateParticipant(expenseId, familyId, participantDto.ParticipantCount);
        return NoContent();
    }
}
