using Microsoft.AspNetCore.Mvc;
using lookbook_dotnet_api.Services;
using Moq;
using Xunit;
using System.Collections.Generic;
using System.Threading.Tasks;
using lookbook_dotnet_api.models;

namespace lookbook_dotnet_api.Tests.Controllers
{
    public class ProdutosControllerTests
    {
        private readonly ProdutosController _controller;
        private readonly Mock<IProdutoService> _produtoServiceMock;

        public ProdutosControllerTests()
        {
            _produtoServiceMock = new Mock<IProdutoService>();
            _controller = new ProdutosController(_produtoServiceMock.Object);
        }

        [Fact]
        public async Task GetProdutos_ShouldReturnOkResult()
        {
            // Arrange
            _produtoServiceMock.Setup(service => service.GetAllProdutosAsync())
                .ReturnsAsync(new List<Produto>());

            // Act
            var result = await _controller.GetProdutos();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var produtos = Assert.IsAssignableFrom<IEnumerable<Produto>>(okResult.Value);
            Assert.Empty(produtos);
        }

        [Fact]
        public async Task CreateProduto_ShouldReturnCreatedAtActionResult()
        {
            // Arrange
            var produto = new Produto { Id = 1, Nome = "Produto 1", Categoria = "Categoria 1" };

            // Act
            var result = await _controller.CreateProduto(produto);

            // Assert
            Assert.IsType<CreatedAtActionResult>(result);
        }

        [Fact]
        public async Task UpdateProduto_ShouldReturnNoContentResult()
        {
            // Arrange
            var produto = new Produto { Id = 1, Nome = "Produto 1", Categoria = "Categoria 1" };

            // Act
            var result = await _controller.UpdateProduto(produto.Id, produto);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteProduto_ShouldReturnNoContentResult()
        {
            // Arrange
            var produtoId = 1;

            // Act
            var result = await _controller.DeleteProduto(produtoId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
