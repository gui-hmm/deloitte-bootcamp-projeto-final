using Xunit;
using Microsoft.EntityFrameworkCore;
using ProjetoFinal.Services;
using ProjetoFinal.Data;
using ProjetoFinal.DTOs;
using ProjetoFinal.Domain.Enums;
using ProjetoFinal.Domain.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

public class EquipamentoServiceTests
{
    private AppDbContext GetContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new AppDbContext(options);
    }

    private EquipamentoService GetService(AppDbContext context)
        => new EquipamentoService(context);

    // ================================
    // CRIAR
    // ================================

    [Fact]
    public async Task CriarAsync_DeveCriarComSucesso()
    {
        var context = GetContext();
        var service = GetService(context);

        var dto = new EquipamentoCreateDto
        {
            Codigo = "COD1",
            Tipo = "Caminhao",
            Modelo = "Modelo X",
            Horimetro = 100,
            StatusOperacional = "Operacional",
            DataAquisicao = DateOnly.FromDateTime(DateTime.Today),
            LocalizacaoAtual = "SP"
        };

        var result = await service.CriarAsync(dto);

        Assert.Equal("COD1", result.Codigo);
        Assert.Single(context.Equipamentos);
    }

    [Fact]
    public async Task CriarAsync_TipoInvalido_DeveLancarExcecao()
    {
        var service = GetService(GetContext());

        var dto = new EquipamentoCreateDto
        {
            Codigo = "COD1",
            Tipo = "INVALIDO",
            Modelo = "Modelo X",
            Horimetro = 100,
            StatusOperacional = "Operacional"
        };

        await Assert.ThrowsAsync<ArgumentException>(() => service.CriarAsync(dto));
    }

    [Fact]
    public async Task CriarAsync_StatusInvalido_DeveLancarExcecao()
    {
        var service = GetService(GetContext());

        var dto = new EquipamentoCreateDto
        {
            Codigo = "COD1",
            Tipo = "Caminhao",
            Modelo = "Modelo X",
            Horimetro = 100,
            StatusOperacional = "INVALIDO"
        };

        await Assert.ThrowsAsync<ArgumentException>(() => service.CriarAsync(dto));
    }

    // ================================
    // LISTAR
    // ================================

    [Fact]
    public async Task ListarAsync_SemFiltros_DeveRetornarTodos()
    {
        var context = GetContext();

        context.Equipamentos.Add(new Equipamento("A", TipoEquipamento.Caminhao, "M1", 10, StatusOperacional.Operacional, null, null));
        context.Equipamentos.Add(new Equipamento("B", TipoEquipamento.Trator, "M2", 20, StatusOperacional.Parado, null, null));
        await context.SaveChangesAsync();

        var service = GetService(context);

        var result = await service.ListarAsync(null, null, null, 1, 10);

        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task ListarAsync_FiltroTipo_DeveFiltrar()
    {
        var context = GetContext();

        context.Equipamentos.Add(new Equipamento("A", TipoEquipamento.Caminhao, "M1", 10, StatusOperacional.Operacional, null, null));
        context.Equipamentos.Add(new Equipamento("B", TipoEquipamento.Trator, "M2", 20, StatusOperacional.Operacional, null, null));
        await context.SaveChangesAsync();

        var service = GetService(context);

        var result = await service.ListarAsync("Caminhao", null, null, 1, 10);

        Assert.Single(result);
    }

    [Fact]
    public async Task ListarAsync_FiltroStatus_DeveFiltrar()
    {
        var context = GetContext();

        context.Equipamentos.Add(new Equipamento("A", TipoEquipamento.Caminhao, "M1", 10, StatusOperacional.Parado, null, null));
        await context.SaveChangesAsync();

        var service = GetService(context);

        var result = await service.ListarAsync(null, "Parado", null, 1, 10);

        Assert.Single(result);
    }

    [Fact]
    public async Task ListarAsync_FiltroCodigo_DeveFiltrar()
    {
        var context = GetContext();

        context.Equipamentos.Add(new Equipamento("ABC123", TipoEquipamento.Caminhao, "M1", 10, StatusOperacional.Operacional, null, null));
        await context.SaveChangesAsync();

        var service = GetService(context);

        var result = await service.ListarAsync(null, null, "ABC", 1, 10);

        Assert.Single(result);
    }

    [Fact]
    public async Task ListarAsync_Paginacao_DeveFuncionar()
    {
        var context = GetContext();

        for (int i = 0; i < 20; i++)
            context.Equipamentos.Add(new Equipamento($"C{i}", TipoEquipamento.Caminhao, "M", 1, StatusOperacional.Operacional, null, null));

        await context.SaveChangesAsync();

        var service = GetService(context);

        var result = await service.ListarAsync(null, null, null, 2, 5);

        Assert.Equal(5, result.Count());
    }

    // ================================
    // OBTER POR ID
    // ================================

    [Fact]
    public async Task ObterPorIdAsync_QuandoExiste()
    {
        var context = GetContext();
        var equipamento = new Equipamento("A", TipoEquipamento.Caminhao, "M", 1, StatusOperacional.Operacional, null, null);
        context.Equipamentos.Add(equipamento);
        await context.SaveChangesAsync();

        var service = GetService(context);

        var result = await service.ObterPorIdAsync(equipamento.Id);

        Assert.NotNull(result);
    }

    [Fact]
    public async Task ObterPorIdAsync_QuandoNaoExiste()
    {
        var service = GetService(GetContext());

        var result = await service.ObterPorIdAsync(999);

        Assert.Null(result);
    }

    // ================================
    // ATUALIZAR
    // ================================

    [Fact]
    public async Task AtualizarAsync_QuandoExiste()
    {
        var context = GetContext();
        var equipamento = new Equipamento("A", TipoEquipamento.Caminhao, "M", 1, StatusOperacional.Operacional, null, null);
        context.Equipamentos.Add(equipamento);
        await context.SaveChangesAsync();

        var service = GetService(context);

        var dto = new EquipamentoUpdateDto
        {
            Horimetro = 200,
            StatusOperacional = "Parado",
            LocalizacaoAtual = "RJ"
        };

        var result = await service.AtualizarAsync(equipamento.Id, dto);

        Assert.True(result);
        Assert.Equal(200, context.Equipamentos.First().Horimetro);
    }

    [Fact]
    public async Task AtualizarAsync_QuandoNaoExiste()
    {
        var service = GetService(GetContext());

        var dto = new EquipamentoUpdateDto
        {
            Horimetro = 10,
            StatusOperacional = "Operacional"
        };

        var result = await service.AtualizarAsync(999, dto);

        Assert.False(result);
    }

    [Fact]
    public async Task AtualizarAsync_StatusInvalido_DeveLancarExcecao()
    {
        var context = GetContext();
        var equipamento = new Equipamento("A", TipoEquipamento.Caminhao, "M", 1, StatusOperacional.Operacional, null, null);
        context.Equipamentos.Add(equipamento);
        await context.SaveChangesAsync();

        var service = GetService(context);

        var dto = new EquipamentoUpdateDto
        {
            Horimetro = 10,
            StatusOperacional = "INVALIDO"
        };

        await Assert.ThrowsAsync<ArgumentException>(() => service.AtualizarAsync(equipamento.Id, dto));
    }

    // ================================
    // REMOVER
    // ================================

    [Fact]
    public async Task RemoverAsync_QuandoExiste()
    {
        var context = GetContext();
        var equipamento = new Equipamento("A", TipoEquipamento.Caminhao, "M", 1, StatusOperacional.Operacional, null, null);
        context.Equipamentos.Add(equipamento);
        await context.SaveChangesAsync();

        var service = GetService(context);

        var result = await service.RemoverAsync(equipamento.Id);

        Assert.True(result);
        Assert.Empty(context.Equipamentos);
    }

    [Fact]
    public async Task RemoverAsync_QuandoNaoExiste()
    {
        var service = GetService(GetContext());

        var result = await service.RemoverAsync(999);

        Assert.False(result);
    }
}