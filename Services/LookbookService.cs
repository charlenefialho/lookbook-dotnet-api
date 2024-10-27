using lookbook_dotnet_api.models;
using lookbook_dotnet_api.Repositories;
using lookbook_dotnet_api.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

public class LookbookService : ILookbookService
{
    private readonly IRepository<Lookbook> _lookbookRepository;
    private readonly IRepository<Produto> _produtoRepository;

    public LookbookService(IRepository<Lookbook> lookbookRepository, IRepository<Produto> produtoRepository)
    {
        _lookbookRepository = lookbookRepository;
        _produtoRepository = produtoRepository;
    }

    public async Task<IEnumerable<Lookbook>> GetAllLookbooksAsync()
    {
        return await _lookbookRepository.GetAllAsync();
    }

    public async Task<Lookbook> CreateLookbookAsync(Lookbook lookbook)
    {
        lookbook.Produtos = await VerificarProdutosAsync(lookbook.Produtos);
        await _lookbookRepository.AddAsync(lookbook);
        return lookbook;
    }

    public async Task UpdateLookbookAsync(int id, Lookbook lookbook)
    {
        var lookbookToUpdate = await _lookbookRepository.GetByIdAsync(id);
        if (lookbookToUpdate == null) throw new KeyNotFoundException("Lookbook não encontrado");

        lookbookToUpdate.Nome = lookbook.Nome;
        lookbookToUpdate.Descricao = lookbook.Descricao;
        lookbookToUpdate.DataCriacao = lookbook.DataCriacao;

        lookbookToUpdate.Produtos = await VerificarProdutosAsync(lookbook.Produtos);
        await _lookbookRepository.UpdateAsync(lookbookToUpdate);
    }

    public async Task DeleteLookbookAsync(int id)
    {
        await _lookbookRepository.DeleteAsync(id);
    }

    private async Task<List<Produto>> VerificarProdutosAsync(IEnumerable<Produto> produtos)
    {
        var produtosVerificados = new List<Produto>();
        foreach (var produto in produtos)
        {
            var produtoExistente = await _produtoRepository.GetByIdAsync(produto.Id);
            produtosVerificados.Add(produtoExistente ?? produto);
        }
        return produtosVerificados;
    }
}
