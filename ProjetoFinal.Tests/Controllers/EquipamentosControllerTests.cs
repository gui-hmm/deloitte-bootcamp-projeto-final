using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using ProjetoFinal.Controllers;
using ProjetoFinal.Services;
using ProjetoFinal.DTOs;

public class EquipamentosControllerTests
{
    private readonly Mock<IEquipamentoService> _mockService;
    private readonly EquipamentosController _controller;

    public EquipamentosControllerTests()
    {
        _mockService = new Mock<IEquipamentoService>();
        _controller = new EquipamentosController(_mockService.Object);
    }

    [Fact]
    public async Task Post_DeveRetornarCreated()
    {
        var dto = new EquipamentoCreateDto();
        var retorno = new { Id = 1 };

        _mockService.Setup(s => s.CriarAsync(dto))
            .ReturnsAsync(new EquipamentoResponseDto
            {
                Id = 1,
                Codigo = "COD1",
                Tipo = "Caminhao",
                Modelo = "Modelo",
                Horimetro = 100,
                StatusOperacional = "Operacional"
            });
        var result = await _controller.Post(dto);

        var created = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(201, created.StatusCode);
    }

    [Fact]
    public async Task Get_DeveRetornarOk()
    {
        _mockService.Setup(s =>
            s.ListarAsync(null, null, null, 1, 10))
            .ReturnsAsync(new List<EquipamentoResponseDto>());

        var result = await _controller.Get(null, null, null);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetById_QuandoExiste_DeveRetornarOk()
    {
        _mockService.Setup(s => s.ObterPorIdAsync(1))
            .ReturnsAsync(new EquipamentoResponseDto());

        var result = await _controller.GetById(1);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetById_QuandoNaoExiste_DeveRetornarNotFound()
    {
        _mockService.Setup(s => s.ObterPorIdAsync(1))
            .ReturnsAsync((EquipamentoResponseDto?)null);

        var result = await _controller.GetById(1);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Put_QuandoAtualiza_DeveRetornarNoContent()
    {
        _mockService.Setup(s => s.AtualizarAsync(1, It.IsAny<EquipamentoUpdateDto>()))
            .ReturnsAsync(true);

        var result = await _controller.Put(1, new EquipamentoUpdateDto());

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task Put_QuandoNaoAtualiza_DeveRetornarNotFound()
    {
        _mockService.Setup(s => s.AtualizarAsync(1, It.IsAny<EquipamentoUpdateDto>()))
            .ReturnsAsync(false);

        var result = await _controller.Put(1, new EquipamentoUpdateDto());

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Delete_QuandoRemove_DeveRetornarNoContent()
    {
        _mockService.Setup(s => s.RemoverAsync(1))
            .ReturnsAsync(true);

        var result = await _controller.Delete(1);

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task Delete_QuandoNaoRemove_DeveRetornarNotFound()
    {
        _mockService.Setup(s => s.RemoverAsync(1))
            .ReturnsAsync(false);

        var result = await _controller.Delete(1);

        Assert.IsType<NotFoundResult>(result);
    }
}