using FilterPagingEfCore.Paging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TripFront.Api.Models;

namespace TripFront.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TripsController : ControllerBase
{
    private readonly ITripService _tripService;

    public TripsController(ITripService tripService)
    {
        _tripService = tripService;
    }

    [HttpGet]
    public async Task<ActionResult<PagingResult<TripDTO>>> GetTrips([FromQuery] PagingRequest paging)
    {
        var result = await _tripService.GetTripsForFamilyAsync(paging.ToPagingParam());
        return Ok(result);
    }

    [HttpGet("{tripId:int}")]
    [AllowAnonymous]
    public async Task<ActionResult<TripDetailsDto>> GetTrip(int tripId)
    {
        var trip = await _tripService.GetTripDetailsAsync(tripId);
        if (trip == null)
        {
            return NotFound();
        }

        return Ok(trip);
    }

    [HttpPost]
    public async Task<ActionResult<TripDTO>> CreateTrip([FromBody] CreateTripDto tripDto)
    {
        var trip = await _tripService.CreateTripAsync(tripDto);
        return CreatedAtAction(nameof(GetTrip), new { tripId = trip.Id }, trip);
    }

    [HttpPut("{tripId:int}")]
    public async Task<IActionResult> UpdateTrip(int tripId, [FromBody] UpdateTripDto tripDto)
    {
        if (tripId != tripDto.Id)
        {
            return BadRequest("Trip id mismatch.");
        }

        await _tripService.Update(tripDto);
        return NoContent();
    }

    [HttpDelete("{tripId:int}")]
    public async Task<IActionResult> DeleteTrip(int tripId)
    {
        await _tripService.Delete(tripId);
        return NoContent();
    }

    [HttpPost("{tripId:int}/paid")]
    public async Task<IActionResult> MarkAsPaid(int tripId)
    {
        await _tripService.Paid(tripId);
        return NoContent();
    }
}
