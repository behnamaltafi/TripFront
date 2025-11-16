using FilterPagingEfCore.Paging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TripFront.Api.Models;

namespace TripFront.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class FamiliesController : ControllerBase
{
    private readonly IFamilyService _familyService;

    public FamiliesController(IFamilyService familyService)
    {
        _familyService = familyService;
    }

    [HttpGet]
    public async Task<ActionResult<PagingResult<FamilyDTO>>> GetFamilies([FromQuery] PagingRequest paging)
    {
        var result = await _familyService.FindAllPaging(paging.ToPagingParam());
        return Ok(result);
    }

    [HttpGet("me")]
    public async Task<ActionResult<FamilyInfo>> GetCurrentFamily()
    {
        var familyId = _familyService.GetFamilyId();
        var family = await _familyService.GetFamilyInfo(familyId);
        if (family == null)
        {
            return NotFound();
        }

        return Ok(family);
    }

    [HttpGet("{id:int}")]
    [AllowAnonymous]
    public async Task<ActionResult<FamilyDTO>> GetFamily(int id)
    {
        var family = await _familyService.Find(id);
        if (family == null)
        {
            return NotFound();
        }

        return Ok(family);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<FamilyDTO>> CreateFamily([FromBody] AddFamilyDTO familyDto)
    {
        var family = await _familyService.Add(familyDto);
        return CreatedAtAction(nameof(GetFamily), new { id = family.Id }, family);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateFamily(int id, [FromBody] UpdateFamilyRequest familyDto)
    {
        if (id != familyDto.Id)
        {
            return BadRequest("Family id mismatch.");
        }

        await _familyService.Update(familyDto);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteFamily(int id)
    {
        await _familyService.Delete(id);
        return NoContent();
    }
}
