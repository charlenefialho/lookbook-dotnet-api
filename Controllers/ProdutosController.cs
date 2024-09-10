using Microsoft.AspNetCore.Mvc;
using lookbook_dotnet_api.models;
using lookbook_dotnet_api.Repositories;

namespace lookbook_dotnet_api.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IRepository<Produto> _produtoRepository;

        public ProdutosController(IRepository<Produto> produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetProdutos()
        {
            var produtos = await _produtoRepository.GetAllAsync();
            return Ok(produtos);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduto([FromBody] Produto produto)
        {
            if (produto == null) return BadRequest();
            await _produtoRepository.AddAsync(produto);
            return CreatedAtAction(nameof(GetProdutos), new { id = produto.Id }, produto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduto(int id, [FromBody] Produto produto)
        {
            if (produto == null || id != produto.Id) return BadRequest();

            var produtoToUpdate = await _produtoRepository.GetByIdAsync(id);
            if (produtoToUpdate == null) return NotFound();

            produtoToUpdate.Nome = produto.Nome;
            produtoToUpdate.Categoria = produto.Categoria;
            // Atualizar outros campos necessários

            await _produtoRepository.UpdateAsync(produtoToUpdate);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduto(int id)
        {
            await _produtoRepository.DeleteAsync(id);
            return NoContent();
        }
    }

}