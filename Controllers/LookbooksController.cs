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

        [HttpPut]
        public async Task<IActionResult> UpdateLookbook([FromBody] Lookbook lookbook)
        {
            if (lookbook == null) return BadRequest();
            await _lookbookRepository.UpdateAsync(lookbook);
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