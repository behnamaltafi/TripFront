using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TripFront.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class SettlementsController : ControllerBase
{
    private readonly ISettlementService _settlementService;

    public SettlementsController(ISettlementService settlementService)
    {
        _settlementService = settlementService;
    }

    [HttpGet("trip/{tripId:int}")]
    public async Task<ActionResult<SettlementReportDto>> GetTripSettlement(int tripId)
    {
        var report = await _settlementService.GetTripSettlementAsync(tripId);
        return Ok(report);
    }

    [HttpGet("global")]
    public async Task<ActionResult<GlobalSettlementDto>> GetGlobalSettlement()
    {
        var report = await _settlementService.GetGlobalSettlementAsync();
        return Ok(report);
    }
}
