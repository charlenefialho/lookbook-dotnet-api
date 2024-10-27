using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using lookbook_dotnet_api.models;
using Swashbuckle.AspNetCore.Annotations;
using lookbook_dotnet_api.Services;

[Route("api/[controller]")]
[ApiController]
public class LookbooksController : ControllerBase
{
    private readonly ILookbookService _lookbookService;

    public LookbooksController(ILookbookService lookbookService)
    {
        _lookbookService = lookbookService;
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Lista todos os lookbooks")]
    [SwaggerResponse(200, "Lista de lookbooks retornada com sucesso", typeof(IEnumerable<Lookbook>))]
    public async Task<IActionResult> GetLookbooks()
    {
        var lookbooks = await _lookbookService.GetAllLookbooksAsync();
        return Ok(lookbooks);
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Cria um novo lookbook")]
    [SwaggerResponse(201, "Lookbook criado com sucesso", typeof(Lookbook))]
    public async Task<IActionResult> CreateLookbook([FromBody] Lookbook lookbook)
    {
        if (lookbook == null) return BadRequest();

        var createdLookbook = await _lookbookService.CreateLookbookAsync(lookbook);
        return CreatedAtAction(nameof(GetLookbooks), new { id = createdLookbook.Id }, createdLookbook);
    }

    [HttpPut("{id}")]
    [SwaggerOperation(Summary = "Atualiza um lookbook existente")]
    public async Task<IActionResult> UpdateLookbook(int id, [FromBody] Lookbook lookbook)
    {
        if (lookbook == null || id != lookbook.Id) return BadRequest();

        await _lookbookService.UpdateLookbookAsync(id, lookbook);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Deleta um lookbook")]
    public async Task<IActionResult> DeleteLookbook(int id)
    {
        await _lookbookService.DeleteLookbookAsync(id);
        return NoContent();
    }
}
