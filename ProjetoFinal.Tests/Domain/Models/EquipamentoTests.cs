using Xunit;
using ProjetoFinal.Domain.Models;
using ProjetoFinal.Domain.Enums;

public class EquipamentoTests
{
    [Fact]
    public void Construtor_DeveCriarCorretamente()
    {
        var equipamento = new Equipamento(
            " COD123 ",
            TipoEquipamento.Caminhao,
            "Modelo X",
            100,
            StatusOperacional.Operacional,
            null,
            "SP");

        Assert.Equal("COD123", equipamento.Codigo);
        Assert.Equal(TipoEquipamento.Caminhao, equipamento.Tipo);
        Assert.Equal("Modelo X", equipamento.Modelo);
        Assert.Equal(100, equipamento.Horimetro);
        Assert.Equal(StatusOperacional.Operacional, equipamento.StatusOperacional);
        Assert.Equal("SP", equipamento.LocalizacaoAtual);
    }

    [Fact]
    public void Atualizar_DeveAlterarCampos()
    {
        var equipamento = new Equipamento(
            "COD1",
            TipoEquipamento.Trator,
            "Modelo Y",
            50,
            StatusOperacional.Operacional,
            null,
            null);

        equipamento.Atualizar(200, StatusOperacional.EmManutencao, "RJ");

        Assert.Equal(200, equipamento.Horimetro);
        Assert.Equal(StatusOperacional.EmManutencao, equipamento.StatusOperacional);
        Assert.Equal("RJ", equipamento.LocalizacaoAtual);
    }
}