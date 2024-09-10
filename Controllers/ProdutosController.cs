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

        [HttpPut]
        public async Task<IActionResult> UpdateProduto([FromBody] Produto produto)
        {
            if (produto == null) return BadRequest();
            await _produtoRepository.UpdateAsync(produto);
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