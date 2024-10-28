using lookbook_dotnet_api.models;
using lookbook_dotnet_api.Repositories;
using lookbook_dotnet_api.Services;
using Moq;
using Xunit;
using System.Collections.Generic;
using System.Threading.Tasks;

    public class ProdutoServiceTests
    {
        private readonly Mock<IRepository<Produto>> _produtoRepositoryMock;
        private readonly ProdutoService _produtoService;

        public ProdutoServiceTests()
        {
            _produtoRepositoryMock = new Mock<IRepository<Produto>>();
            _produtoService = new ProdutoService(_produtoRepositoryMock.Object);
        }

        [Fact]
        public async Task GetAllProdutosAsync_ShouldReturnAllProdutos()
        {
            // Arrange
            var produtos = new List<Produto>
            {
                new Produto { Id = 1, Nome = "Produto 1", Categoria = "Categoria 1" },
                new Produto { Id = 2, Nome = "Produto 2", Categoria = "Categoria 2" }
            };
            _produtoRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(produtos);

            // Act
            var result = await _produtoService.GetAllProdutosAsync();

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task CreateProdutoAsync_ShouldCallAddMethod()
        {
            // Arrange
            var produto = new Produto { Id = 3, Nome = "Produto 3", Categoria = "Categoria 3" };

            // Act
            await _produtoService.CreateProdutoAsync(produto);

            // Assert
            _produtoRepositoryMock.Verify(repo => repo.AddAsync(produto), Times.Once);
        }

        [Fact]
        public async Task GetProdutoByIdAsync_ProdutoExists_ShouldReturnProduto()
        {
            // Arrange
            var produto = new Produto { Id = 1, Nome = "Produto 1", Categoria = "Categoria 1" };
            _produtoRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(produto);

            // Act
            var result = await _produtoService.GetProdutoByIdAsync(1);

            // Assert
            Assert.Equal(produto, result);
        }

        [Fact]
        public async Task GetProdutoByIdAsync_ProdutoNotFound_ShouldReturnNull()
        {
            // Arrange
            _produtoRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Produto)null);

            // Act
            var result = await _produtoService.GetProdutoByIdAsync(999);

            // Assert
            Assert.Null(result);
        }

}
