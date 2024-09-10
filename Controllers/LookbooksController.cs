using Microsoft.AspNetCore.Mvc;
using lookbook_dotnet_api.models;
using lookbook_dotnet_api.Repositories;

namespace lookbook_dotnet_api.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class LookbooksController : ControllerBase
    {
        private readonly IRepository<Lookbook> _lookbookRepository;

        public LookbooksController(IRepository<Lookbook> lookbookRepository)
        {
            _lookbookRepository = lookbookRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetLookbooks()
        {
            var lookbooks = await _lookbookRepository.GetAllAsync();
            return Ok(lookbooks);
        }

        [HttpPost]
        public async Task<IActionResult> CreateLookbook([FromBody] Lookbook lookbook)
        {
            if (lookbook == null) return BadRequest();
            await _lookbookRepository.AddAsync(lookbook);
            return CreatedAtAction(nameof(GetLookbooks), new { id = lookbook.Id }, lookbook);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLookbook(int id, [FromBody] Lookbook lookbook)
        {
            if (lookbook == null || id != lookbook.Id) return BadRequest();

            var lookbookToUpdate = await _lookbookRepository.GetByIdAsync(id);
            if (lookbookToUpdate == null) return NotFound();

            lookbookToUpdate.Nome = lookbook.Nome;
            lookbookToUpdate.Descricao = lookbook.Descricao;
            lookbookToUpdate.DataCriacao = lookbook.DataCriacao;
            // Atualizar outros campos necessários

            await _lookbookRepository.UpdateAsync(lookbookToUpdate);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLookbook(int id)
        {
            await _lookbookRepository.DeleteAsync(id);
            return NoContent();
        }
    }

}