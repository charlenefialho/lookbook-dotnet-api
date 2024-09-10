using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using lookbook_dotnet_api.models;
using lookbook_dotnet_api.services;

namespace lookbook_dotnet_api.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly ProdutoService _produtoService;

        public ProdutoController(ProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> GetProdutos()
        {
            return Ok(await _produtoService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Produto>> GetProduto(int id)
        {
            var produto = await _produtoService.GetById(id);
            if (produto == null) return NotFound();
            return Ok(produto);
        }

        [HttpPost]
        public async Task<ActionResult> CreateProduto([FromBody] Produto produto)
        {
            // Garantir que LookbookProdutos seja inicializado
            if (produto.LookbookProdutos == null)
            {
                produto.LookbookProdutos = new List<LookbookProduto>();
            }

            await _produtoService.Create(produto);
            return CreatedAtAction(nameof(GetProduto), new { id = produto.Id }, produto);
        }



        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduto(int id, [FromBody] Produto produto)
        {
            if (id != produto.Id) return BadRequest();
            await _produtoService.Update(produto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduto(int id)
        {
            await _produtoService.Delete(id);
            return NoContent();
        }
    }

}