using lookbook_dotnet_api.models;
using lookbook_dotnet_api.Repositories;
using lookbook_dotnet_api.Services;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

public class LookbookServiceTests
{
    private readonly Mock<IRepository<Lookbook>> _lookbookRepositoryMock;
    private readonly Mock<IRepository<Produto>> _produtoRepositoryMock;
    private readonly LookbookService _lookbookService;

    public LookbookServiceTests()
    {
        _lookbookRepositoryMock = new Mock<IRepository<Lookbook>>();
        _produtoRepositoryMock = new Mock<IRepository<Produto>>();
        _lookbookService = new LookbookService(_lookbookRepositoryMock.Object, _produtoRepositoryMock.Object);
    }

    [Fact]
    public async Task GetAllLookbooksAsync_ShouldReturnAllLookbooks()
    {
        // Arrange
        var lookbooks = new List<Lookbook>
        {
            new Lookbook { Id = 1, Nome = "Lookbook 1" },
            new Lookbook { Id = 2, Nome = "Lookbook 2" }
        };
        _lookbookRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(lookbooks);

        // Act
        var result = await _lookbookService.GetAllLookbooksAsync();

        // Assert
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task CreateLookbookAsync_ShouldAddLookbookWithVerifiedProdutos()
    {
        // Arrange
        var produtos = new List<Produto>
        {
            new Produto { Id = 1, Nome = "Produto 1" },
            new Produto { Id = 2, Nome = "Produto 2" }
        };
        var lookbook = new Lookbook { Id = 1, Nome = "Lookbook 1", Produtos = produtos };

        _produtoRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((int id) => produtos.FirstOrDefault(p => p.Id == id));

        // Act
        var result = await _lookbookService.CreateLookbookAsync(lookbook);

        // Assert
        _lookbookRepositoryMock.Verify(repo => repo.AddAsync(lookbook), Times.Once);
        Assert.Equal(lookbook, result);
    }

    [Fact]
    public async Task UpdateLookbookAsync_ShouldUpdateLookbookWithVerifiedProdutos()
    {
        // Arrange
        var produtos = new List<Produto>
        {
            new Produto { Id = 1, Nome = "Produto 1" },
            new Produto { Id = 2, Nome = "Produto 2" }
        };
        var lookbook = new Lookbook { Id = 1, Nome = "Lookbook Atualizado", Produtos = produtos };
        var existingLookbook = new Lookbook { Id = 1, Nome = "Lookbook Antigo", Produtos = new List<Produto>() };

        _lookbookRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(existingLookbook);
        _produtoRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((int id) => produtos.FirstOrDefault(p => p.Id == id));

        // Act
        await _lookbookService.UpdateLookbookAsync(1, lookbook);

        // Assert
        _lookbookRepositoryMock.Verify(repo => repo.UpdateAsync(existingLookbook), Times.Once);
        Assert.Equal("Lookbook Atualizado", existingLookbook.Nome);
    }

    [Fact]
    public async Task DeleteLookbookAsync_ShouldCallDeleteMethod()
    {
        // Arrange
        var lookbookId = 1;

        // Act
        await _lookbookService.DeleteLookbookAsync(lookbookId);

        // Assert
        _lookbookRepositoryMock.Verify(repo => repo.DeleteAsync(lookbookId), Times.Once);
    }

    [Fact]
    public async Task CreateLookbookAsync_ShouldAddProdutosIfNotExistInRepository()
    {
        // Arrange
        var produtos = new List<Produto>
        {
            new Produto { Id = 1, Nome = "Produto Existente" },
            new Produto { Id = 3, Nome = "Produto Novo" }
        };
        var lookbook = new Lookbook { Id = 1, Nome = "Lookbook Teste", Produtos = produtos };

        _produtoRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((int id) => id == 1 ? produtos.First() : null);

        // Act
        var result = await _lookbookService.CreateLookbookAsync(lookbook);

        // Assert
        _lookbookRepositoryMock.Verify(repo => repo.AddAsync(lookbook), Times.Once);
        Assert.Equal(lookbook, result);
        Assert.Contains(result.Produtos, p => p.Id == 3);
    }
}
