using FilterPagingEfCore.Paging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TripFront.Api.Models;
using TripFront.Models;

namespace TripFront.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class FriendshipsController : ControllerBase
{
    private readonly IFriendShipService _friendShipService;

    public FriendshipsController(IFriendShipService friendShipService)
    {
        _friendShipService = friendShipService;
    }

    [HttpGet]
    public async Task<ActionResult<PagingResult<FamilyDTO>>> GetFriends([FromQuery] PagingRequest paging, [FromQuery] int familyId)
    {
        if (familyId <= 0)
        {
            return BadRequest("familyId is required.");
        }

        var result = await _friendShipService.GetFriendsAsync(paging.ToPagingParam(), familyId);
        return Ok(result);
    }

    [HttpGet("status/{targetFamilyId:int}")]
    public async Task<ActionResult<bool>> AreFriends(int targetFamilyId)
    {
        var areFriends = await _friendShipService.AreFriendsAsync(targetFamilyId);
        return Ok(areFriends);
    }

    [HttpGet("{targetFamilyId:int}")]
    public async Task<ActionResult<FamilyFriendship>> GetFriendship(int targetFamilyId)
    {
        var friendship = await _friendShipService.GetFriendshipAsync(targetFamilyId);
        if (friendship == null)
        {
            return NotFound();
        }

        return Ok(friendship);
    }

    [HttpPost]
    public async Task<ActionResult<FamilyFriendship>> CreateFriendship([FromBody] CreateFriendshipRequest request)
    {
        var friendship = new FamilyFriendship
        {
            FamilyId = request.FamilyId,
            FriendFamilyId = request.FriendFamilyId,
            Status = request.Status,
            FriendshipDate = request.FriendshipDate ?? DateTime.UtcNow,
            RequestMessage = request.RequestMessage
        };

        await _friendShipService.AddFriendshipAsync(friendship);
        return CreatedAtAction(nameof(GetFriendship), new { targetFamilyId = request.FriendFamilyId }, friendship);
    }

    [HttpDelete("{targetFamilyId:int}")]
    public async Task<IActionResult> DeleteFriendship(int targetFamilyId)
    {
        var friendship = await _friendShipService.GetFriendshipAsync(targetFamilyId);
        if (friendship == null)
        {
            return NotFound();
        }

        await _friendShipService.RemoveFriendshipAsync(friendship);
        return NoContent();
    }
}
