using System.Collections.Generic;
using System.Threading.Tasks;
using lookbook_dotnet_api.models;
using lookbook_dotnet_api.services;
using Microsoft.AspNetCore.Mvc;

namespace lookbook_dotnet_api.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LookbookController : ControllerBase
    {
        private readonly LookbookService _lookbookService;

        public LookbookController(LookbookService lookbookService)
        {
            _lookbookService = lookbookService;
        }

        // GET: api/Lookbook
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lookbook>>> GetLookbooks()
        {
            var lookbooks = await _lookbookService.GetAll();
            return Ok(lookbooks);
        }

        // GET: api/Lookbook/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Lookbook>> GetLookbook(int id)
        {
            var lookbook = await _lookbookService.GetById(id);

            if (lookbook == null)
            {
                return NotFound();
            }

            return Ok(lookbook);
        }

        // POST: api/Lookbook
        [HttpPost]
        public async Task<ActionResult<Lookbook>> PostLookbook(Lookbook lookbook)
        {
            // Inicializa a coleção se for nula
            if (lookbook.LookbookProdutos == null)
            {
                lookbook.LookbookProdutos = new List<LookbookProduto>();
            }

            // Valida os produtos associados
            foreach (var lp in lookbook.LookbookProdutos)
            {
                if (lp.ProdutoId <= 0)
                {
                    ModelState.AddModelError("LookbookProdutos", "ProdutoId deve ser um valor válido.");
                    return BadRequest(ModelState);
                }
            }

            await _lookbookService.Create(lookbook);
            return CreatedAtAction(nameof(GetLookbook), new { id = lookbook.Id }, lookbook);
        }


        // PUT: api/Lookbook/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLookbook(int id, Lookbook lookbook)
        {
            if (id != lookbook.Id)
            {
                return BadRequest();
            }

            await _lookbookService.Update(lookbook);
            return NoContent();
        }

        // DELETE: api/Lookbook/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLookbook(int id)
        {
            await _lookbookService.Delete(id);
            return NoContent();
        }
    }
}