using lookbook_dotnet_api.models;
using lookbook_dotnet_api.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

[Route("api/[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly IProdutoService _produtoService;

    public ProdutosController(IProdutoService produtoService)
    {
        _produtoService = produtoService;
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Lista todos os produtos",
        Description = "Recupera a lista de todos os produtos existentes."
    )]
    [SwaggerResponse(200, "Lista de produtos retornada com sucesso", typeof(IEnumerable<Produto>))]
    public async Task<IActionResult> GetProdutos()
    {
        var produtos = await _produtoService.GetAllProdutosAsync();
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

        await _produtoService.CreateProdutoAsync(produto);
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

        try
        {
            await _produtoService.UpdateProdutoAsync(produto);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
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
        try
        {
            await _produtoService.DeleteProdutoAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}
