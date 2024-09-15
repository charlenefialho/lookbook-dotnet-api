using lookbook_dotnet_api.models;
using lookbook_dotnet_api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace lookbook_dotnet_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LookbooksController : ControllerBase
    {
        private readonly IRepository<Lookbook> _lookbookRepository;
        private readonly IRepository<Produto> _produtoRepository;

        public LookbooksController(IRepository<Lookbook> lookbookRepository, IRepository<Produto> produtoRepository)
        {
            _lookbookRepository = lookbookRepository;
            _produtoRepository = produtoRepository;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Lista todos os lookbooks",
            Description = "Recupera a lista de todos os lookbooks existentes."
        )]
        [SwaggerResponse(200, "Lista de lookbooks retornada com sucesso", typeof(IEnumerable<Lookbook>))]
        public async Task<IActionResult> GetLookbooks()
        {
            var lookbooks = await _lookbookRepository.GetAllAsync();
            return Ok(lookbooks);
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Cria um novo lookbook",
            Description = "Adiciona um novo lookbook ao banco de dados."
        )]
        [SwaggerResponse(201, "Lookbook criado com sucesso", typeof(Lookbook))]
        public async Task<IActionResult> CreateLookbook([FromBody] Lookbook lookbook)
        {
            if (lookbook == null)
                return BadRequest();

            // Verifica se cada produto já existe no banco de dados
            var produtosNoLookbook = new List<Produto>();

            foreach (var produto in lookbook.Produtos)
            {
                var produtoExistente = await _produtoRepository.GetByIdAsync(produto.Id);

                if (produtoExistente != null)
                {
                    // Produto já existe, associa ao lookbook
                    produtosNoLookbook.Add(produtoExistente);
                }
                else
                {
                    // Produto não existe, adiciona à lista para ser criado
                    produtosNoLookbook.Add(produto);
                }
            }

            // Atualiza os produtos do lookbook com a lista verificada
            lookbook.Produtos = produtosNoLookbook;

            // Adiciona o lookbook ao banco de dados
            await _lookbookRepository.AddAsync(lookbook);

            return CreatedAtAction(nameof(GetLookbooks), new { id = lookbook.Id }, lookbook);
        }


        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Atualiza um lookbook existente",
            Description = "Atualiza os dados de um lookbook existente no banco de dados."
        )]
        [SwaggerResponse(204, "Lookbook atualizado com sucesso")]
        [SwaggerResponse(400, "Requisição inválida")]
        [SwaggerResponse(404, "Lookbook não encontrado")]
        public async Task<IActionResult> UpdateLookbook(int id, [FromBody] Lookbook lookbook)
        {
            if (lookbook == null || id != lookbook.Id)
                return BadRequest();

            var lookbookToUpdate = await _lookbookRepository.GetByIdAsync(id);
            if (lookbookToUpdate == null)
                return NotFound();

            lookbookToUpdate.Nome = lookbook.Nome;
            lookbookToUpdate.Descricao = lookbook.Descricao;
            lookbookToUpdate.DataCriacao = lookbook.DataCriacao;

            await _lookbookRepository.UpdateAsync(lookbookToUpdate);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Deleta um lookbook",
            Description = "Remove um lookbook do banco de dados."
        )]
        [SwaggerResponse(204, "Lookbook removido com sucesso")]
        [SwaggerResponse(404, "Lookbook não encontrado")]
        public async Task<IActionResult> DeleteLookbook(int id)
        {
            var lookbook = await _lookbookRepository.GetByIdAsync(id);
            if (lookbook == null)
                return NotFound();

            await _lookbookRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
