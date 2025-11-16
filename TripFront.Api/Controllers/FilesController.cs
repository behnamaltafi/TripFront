using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace TripFront.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class FilesController : ControllerBase
{
    private readonly IFileStorageService _fileStorageService;

    public FilesController(IFileStorageService fileStorageService)
    {
        _fileStorageService = fileStorageService;
    }

    [HttpPost]
    [RequestSizeLimit(10 * 1024 * 1024)]
    public async Task<ActionResult<object>> Upload([FromForm] IFormFile file, [FromForm] string? subDirectory)
    {
        if (file == null)
        {
            return BadRequest("File is required.");
        }

        var path = await _fileStorageService.SaveFileAsync(file, subDirectory);
        return Ok(new { path });
    }

    [HttpGet]
    public async Task<IActionResult> Download([FromQuery] string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            return BadRequest("File path is required.");
        }

        var stream = await _fileStorageService.GetFileAsync(path);
        var fileName = Path.GetFileName(path);
        return File(stream, "application/octet-stream", fileName);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            return BadRequest("File path is required.");
        }

        await _fileStorageService.DeleteFileAsync(path);
        return NoContent();
    }
}
