using lookbook_dotnet_api.models;
using lookbook_dotnet_api.Repositories;

namespace lookbook_dotnet_api.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IRepository<Produto> _produtoRepository;

        public ProdutoService(IRepository<Produto> produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task<IEnumerable<Produto>> GetAllProdutosAsync()
        {
            return await _produtoRepository.GetAllAsync();
        }

        public async Task<Produto> GetProdutoByIdAsync(int id)
        {
            return await _produtoRepository.GetByIdAsync(id);
        }

        public async Task CreateProdutoAsync(Produto produto)
        {
            await _produtoRepository.AddAsync(produto);
        }

        public async Task UpdateProdutoAsync(Produto produto)
        {
            var produtoExistente = await _produtoRepository.GetByIdAsync(produto.Id);
            if (produtoExistente == null)
                throw new KeyNotFoundException("Produto não encontrado");

            produtoExistente.Nome = produto.Nome;
            produtoExistente.Categoria = produto.Categoria;

            await _produtoRepository.UpdateAsync(produtoExistente);
        }

        public async Task DeleteProdutoAsync(int id)
        {
            var produto = await _produtoRepository.GetByIdAsync(id);
            if (produto == null)
                throw new KeyNotFoundException("Produto não encontrado");

            await _produtoRepository.DeleteAsync(id);
        }
    }

}
