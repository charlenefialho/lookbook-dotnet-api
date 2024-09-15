using lookbook_dotnet_api.models;
using lookbook_dotnet_api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using lookbook_dotnet_api.data;
using Microsoft.EntityFrameworkCore;

namespace lookbook_dotnet_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LookbooksController : ControllerBase
    {
        private readonly IRepository<Lookbook> _lookbookRepositoryGeneric;
        private readonly IRepository<Produto> _produtoRepository;

        private readonly ILookbookRepository _lookbookRepository;

        private readonly ApplicationDbContext _context;



        public LookbooksController(IRepository<Lookbook> lookbookRepositoryGeneric, IRepository<Produto> produtoRepository, ILookbookRepository lookbookRepository, ApplicationDbContext context)
        {
            _context = context;
            _lookbookRepositoryGeneric = lookbookRepositoryGeneric;
            _produtoRepository = produtoRepository;
            _lookbookRepository = lookbookRepository;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Lista todos os lookbooks",
            Description = "Recupera a lista de todos os lookbooks existentes."
        )]
        [SwaggerResponse(200, "Lista de lookbooks retornada com sucesso", typeof(IEnumerable<Lookbook>))]
        public async Task<IActionResult> GetLookbooks()
        {
            var lookbooks = await _lookbookRepository.GetLookbooksWithProdutosAsync();
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
            await _lookbookRepositoryGeneric.AddAsync(lookbook);

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

            var lookbookToUpdate = await _lookbookRepositoryGeneric.GetByIdAsync(id);
            if (lookbookToUpdate == null)
                return NotFound();

            // Atualiza os dados básicos do lookbook
            lookbookToUpdate.Nome = lookbook.Nome;
            lookbookToUpdate.Descricao = lookbook.Descricao;
            lookbookToUpdate.DataCriacao = lookbook.DataCriacao;

            // Obtém a lista atual de produtos do lookbook
            var produtosAtuais = lookbookToUpdate.Produtos.ToList();

            // Obtém a lista de produtos a serem atualizados
            var produtosParaAtualizar = new List<Produto>();

            foreach (var produto in lookbook.Produtos)
            {
                var produtoExistente = await _produtoRepository.GetByIdAsync(produto.Id);
                if (produtoExistente != null)
                {
                    produtosParaAtualizar.Add(produtoExistente);
                }
                else
                {
                    produtosParaAtualizar.Add(produto);
                }
            }

            // Identifica produtos que precisam ser removidos
            var produtosParaRemover = produtosAtuais
                .Where(p => !produtosParaAtualizar.Any(np => np.Id == p.Id))
                .ToList();

            // Atualiza a lista de produtos do lookbook
            lookbookToUpdate.Produtos = produtosParaAtualizar;

            // Atualiza o lookbook no banco de dados
            await _lookbookRepositoryGeneric.UpdateAsync(lookbookToUpdate);

            // Remove associações de produtos não associados
            foreach (var produto in produtosParaRemover)
            {
                // Remove a associação na tabela de junção
                _context.Entry(produto).State = EntityState.Detached;
            }

            await _context.SaveChangesAsync(); // Salva as mudanças no banco de dados

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
            var lookbook = await _lookbookRepositoryGeneric.GetByIdAsync(id);
            if (lookbook == null)
                return NotFound();

            await _lookbookRepositoryGeneric.DeleteAsync(id);
            return NoContent();
        }
    }
}
