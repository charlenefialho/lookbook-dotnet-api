using lookbook_dotnet_api.models;
using lookbook_dotnet_api.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

public class LookbooksControllerTests
{
    private readonly Mock<ILookbookService> _lookbookServiceMock;
    private readonly LookbooksController _lookbooksController;

    public LookbooksControllerTests()
    {
        _lookbookServiceMock = new Mock<ILookbookService>();
        _lookbooksController = new LookbooksController(_lookbookServiceMock.Object);
    }

    [Fact]
    public async Task GetLookbooks_ShouldReturnOkWithLookbooks()
    {
        // Arrange
        var lookbooks = new List<Lookbook>
        {
            new Lookbook { Id = 1, Nome = "Lookbook 1" },
            new Lookbook { Id = 2, Nome = "Lookbook 2" }
        };
        _lookbookServiceMock.Setup(service => service.GetAllLookbooksAsync()).ReturnsAsync(lookbooks);

        // Act
        var result = await _lookbooksController.GetLookbooks();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<Lookbook>>(okResult.Value);
        Assert.Equal(2, returnValue.Count);
    }

    [Fact]
    public async Task CreateLookbook_ValidLookbook_ShouldReturnCreatedAtAction()
    {
        // Arrange
        var lookbook = new Lookbook { Id = 1, Nome = "Novo Lookbook" };
        _lookbookServiceMock.Setup(service => service.CreateLookbookAsync(It.IsAny<Lookbook>())).ReturnsAsync(lookbook);

        // Act
        var result = await _lookbooksController.CreateLookbook(lookbook);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        var returnValue = Assert.IsType<Lookbook>(createdAtActionResult.Value);
        Assert.Equal(lookbook.Id, returnValue.Id);
        Assert.Equal("Novo Lookbook", returnValue.Nome);
    }

    [Fact]
    public async Task CreateLookbook_NullLookbook_ShouldReturnBadRequest()
    {
        // Act
        var result = await _lookbooksController.CreateLookbook(null);

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task UpdateLookbook_ValidLookbook_ShouldReturnNoContent()
    {
        // Arrange
        var lookbook = new Lookbook { Id = 1, Nome = "Lookbook Atualizado" };

        // Act
        var result = await _lookbooksController.UpdateLookbook(1, lookbook);

        // Assert
        Assert.IsType<NoContentResult>(result);
        _lookbookServiceMock.Verify(service => service.UpdateLookbookAsync(1, lookbook), Times.Once);
    }

    [Fact]
    public async Task UpdateLookbook_InvalidLookbook_ShouldReturnBadRequest()
    {
        // Arrange
        var lookbook = new Lookbook { Id = 2, Nome = "Lookbook Inválido" };

        // Act
        var result = await _lookbooksController.UpdateLookbook(1, lookbook);

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task DeleteLookbook_ValidId_ShouldReturnNoContent()
    {
        // Act
        var result = await _lookbooksController.DeleteLookbook(1);

        // Assert
        Assert.IsType<NoContentResult>(result);
        _lookbookServiceMock.Verify(service => service.DeleteLookbookAsync(1), Times.Once);
    }
}