using lookbook_dotnet_api.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class PhotosController : ControllerBase
{
    private readonly IPexelsService _pexelsService;

    public PhotosController(IPexelsService pexelsService)
    {
        _pexelsService = pexelsService;
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchPhotos(string query)
    {
        try
        {
            var result = await _pexelsService.SearchPhotosAsync(query);
            return Ok(result);
        }
        catch (ApplicationException ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}

