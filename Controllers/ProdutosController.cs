using Microsoft.AspNetCore.Mvc;
using lookbook_dotnet_api.models;
using lookbook_dotnet_api.Repositories;
using Swashbuckle.AspNetCore.Annotations;

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
        [SwaggerOperation(
            Summary = "Lista todos os produtos",
            Description = "Recupera a lista de todos os produtos existentes."
        )]
        [SwaggerResponse(200, "Lista de produtos retornada com sucesso", typeof(IEnumerable<Produto>))]
        public async Task<IActionResult> GetProdutos()
        {
            var produtos = await _produtoRepository.GetAllAsync();
            return Ok(produtos);
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Cria um novo produto",
            Description = "Adiciona um novo produto ao banco de dados."
        )]
        [SwaggerResponse(201, "Produto criado com sucesso", typeof(Produto))]
        public async Task<IActionResult> CreateProduto([FromBody] Produto produto)
        {
            if (produto == null) return BadRequest();
            await _produtoRepository.AddAsync(produto);
            return CreatedAtAction(nameof(GetProdutos), new { id = produto.Id }, produto);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Atualiza um produto existente",
            Description = "Atualiza os dados de um produto existente no banco de dados."
        )]
        [SwaggerResponse(204, "Produto atualizado com sucesso")]
        [SwaggerResponse(400, "Requisição inválida")]
        [SwaggerResponse(404, "Produto não encontrado")]
        public async Task<IActionResult> UpdateProduto(int id, [FromBody] Produto produto)
        {
            if (produto == null || id != produto.Id) return BadRequest();

            var produtoToUpdate = await _produtoRepository.GetByIdAsync(id);
            //if (produtoToUpdate == null) return NotFound();
            if (produtoToUpdate == null)
            {
                return BadRequest();
            }
            else
            {
                produtoToUpdate.Nome = produto.Nome;
                produtoToUpdate.Categoria = produto.Categoria;

                await _produtoRepository.UpdateAsync(produtoToUpdate);
                return NoContent();
            }

        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Deleta um produto",
            Description = "Remove um produto do banco de dados."
        )]
        [SwaggerResponse(204, "Produto removido com sucesso")]
        [SwaggerResponse(404, "Produto não encontrado")]
        public async Task<IActionResult> DeleteProduto(int id)
        {
            var produto = await _produtoRepository.GetByIdAsync(id);
            if (produto == null)
                return NotFound();

            await _produtoRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
