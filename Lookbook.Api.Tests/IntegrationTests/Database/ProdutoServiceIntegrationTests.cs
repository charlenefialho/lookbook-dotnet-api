using Microsoft.EntityFrameworkCore;
using lookbook_dotnet_api.data;
using lookbook_dotnet_api.models;
using lookbook_dotnet_api.Repositories;
using lookbook_dotnet_api.Services;
using Xunit;
using System.Threading.Tasks;

public class ProdutoServiceIntegrationTests
{
    private readonly ProdutoService _produtoService;
    private readonly ApplicationDbContext _dbContext;

    public ProdutoServiceIntegrationTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase" + Guid.NewGuid())
            .Options;

        _dbContext = new ApplicationDbContext(options);
        var produtoRepository = new Repository<Produto>(_dbContext);
        _produtoService = new ProdutoService(produtoRepository);
    }

    [Fact]
    public async Task CreateProdutoAsync_ShouldAddProdutoToDatabase()
    {
        // Arrange
        var produto = new Produto { Nome = "Produto de Teste", Categoria = "Categoria de Teste" };

        // Act
        await _produtoService.CreateProdutoAsync(produto);

        // Assert
        var produtoFromDb = await _dbContext.Produtos.FirstOrDefaultAsync(p => p.Nome == "Produto de Teste");
        Assert.NotNull(produtoFromDb);
    }

    [Fact]
    public async Task GetAllProdutosAsync_ShouldReturnProdutosFromDatabase()
    {
        // Arrange
        _dbContext.Produtos.Add(new Produto { Nome = "Produto 1", Categoria = "Categoria 1" });
        _dbContext.Produtos.Add(new Produto { Nome = "Produto 2", Categoria = "Categoria 2" });
        await _dbContext.SaveChangesAsync();

        // Act
        var produtos = await _produtoService.GetAllProdutosAsync();

        // Assert
        Assert.Equal(2, produtos.Count());
    }
}
